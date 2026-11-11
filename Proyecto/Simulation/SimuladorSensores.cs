using Proyecto.Domain;
using Proyecto.DynamicStructure;
using Proyecto.Exceptions;
using Proyecto.Infraestructure;

namespace Proyecto.Simulation
{
    // Genera lecturas falsas pero coherentes para cada sensor activo, una vez por "paso".
    //
    // Cómo funciona:
    //   1. Iniciar() arranca un ciclo asíncrono (BucleAsync) que no congela la ventana.
    //   2. En cada vuelta espera IntervaloMs con await Task.Delay y luego ejecuta Paso().
    //   3. Paso() avanza el reloj simulado, calcula el clima y la humedad de cada zona
    //      y mete una lectura por sensor en una Cola.
    //   4. Cada PasosPorGuardado pasos, Guardar() vacía la cola en el repositorio y
    //      escribe el archivo de una sola vez.
    //
    // Todo corre en el hilo de la interfaz: await no crea hilos, solo "pausa" el método
    // sin bloquear. Por eso no hay dos códigos tocando la Lista del repositorio a la vez.
    internal class SimuladorSensores
    {
        #region Singleton
        private static SimuladorSensores? instance;

        public static SimuladorSensores GetInstance()
        {
            if (instance == null)
            {
                instance = new SimuladorSensores();
            }
            return instance;
        }
        #endregion

        // Cuánto tiempo simulado avanza cada paso.
        public static readonly TimeSpan PasoSimulado = TimeSpan.FromMinutes(10);
        public const int PasosPorGuardado = 5;

        private readonly Random _azar = new Random();
        private readonly Cola<Lectura> _pendientes = new Cola<Lectura>();
        private readonly Lista<EstadoZona> _estados = new Lista<EstadoZona>();
        private Lista<Lectura> _ultimas = new Lista<Lectura>();

        // Últimos avisos: al pasar de MaxHistorial se saca el más viejo (el del frente de la cola).
        public const int MaxHistorial = 200;
        private readonly Cola<string> _historial = new Cola<string>();

        private CancellationTokenSource? _cancelar;
        private long _siguienteId;
        private int _pasosSinGuardar;

        // Clima: es el mismo para todas las zonas.
        private int _pasosDeLluviaRestantes;
        private double _intensidadLluvia;      // mm por paso

        public int IntervaloMs { get; set; } = 1000;
        public DateTime Reloj { get; private set; } = DateTime.Now;
        public bool EnMarcha => _cancelar != null;
        public bool Lloviendo => _pasosDeLluviaRestantes > 0;
        public long TotalGeneradas { get; private set; }
        public int EnCola => _pendientes.Count;
        public IEnumerable<Lectura> UltimasLecturas => _ultimas;
        public IEnumerable<string> Historial => _historial;

        // Avisos para la interfaz. Action es un delegado: guarda métodos para llamarlos luego.
        public event Action? PasoTerminado;
        public event Action<string>? Aviso;

        private SimuladorSensores() { }

        public void Iniciar()
        {
            if (EnMarcha)
            {
                return;
            }

            _cancelar = new CancellationTokenSource();
            _siguienteId = RepositoryLectura.GetInstance().SiguienteId();

            // No se espera (await) aquí: el ciclo queda corriendo y Iniciar termina enseguida.
            _ = BucleAsync(_cancelar.Token);
            Avisar($"{Reloj:HH:mm} Simulación iniciada");
        }

        public void Detener()
        {
            if (_cancelar == null)
            {
                return;
            }

            _cancelar.Cancel();
            _cancelar.Dispose();
            _cancelar = null;

            // Lo que quedó en la cola se guarda para no perderlo.
            Guardar();
            Avisar($"{Reloj:HH:mm} Simulación detenida, lecturas guardadas");
        }

        private async Task BucleAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    // Aquí el método se "pausa" y la ventana sigue respondiendo.
                    await Task.Delay(IntervaloMs, token);

                    Paso();

                    _pasosSinGuardar++;
                    if (_pasosSinGuardar >= PasosPorGuardado)
                    {
                        Guardar();
                    }

                    PasoTerminado?.Invoke();
                }
            }
            catch (OperationCanceledException)
            {
                // Detener() canceló la espera: es la forma normal de salir.
            }
            catch (Exception ex)
            {
                // Nadie espera (await) esta tarea: si no se atrapa aquí, el error se perdería
                // en silencio y el simulador quedaría "en marcha" sin hacer nada.
                Detener();
                Avisar($"La simulación se detuvo por un error: {ex.Message}");
                PasoTerminado?.Invoke();
            }
        }

        // Un paso de simulación. Es público para poder probarlo sin esperar.
        public void Paso()
        {
            if (_siguienteId == 0)
            {
                _siguienteId = RepositoryLectura.GetInstance().SiguienteId();
            }

            Reloj += PasoSimulado;
            double temperatura = Temperatura(Reloj);
            double lluvia = Llover();

            Lista<Lectura> nuevas = new Lista<Lectura>();
            RepositorySensor sensores = RepositorySensor.GetInstance();

            foreach (ZonaVerde zona in RepositoryZona.GetInstance().GetAll())
            {
                if (!zona.Activo)
                {
                    continue;
                }

                EstadoZona estado = EstadoDe(zona);
                ActualizarHumedad(zona, estado, temperatura, lluvia);

                foreach (Sensor sensor in sensores.GetByZona(zona.Id))
                {
                    if (!sensor.Activo)
                    {
                        continue;
                    }

                    double valor = Math.Round(Medir(sensor, zona, estado, temperatura, lluvia), 2);
                    Lectura lectura = Lectura.Create(_siguienteId++, sensor.Id, Reloj, valor);

                    _pendientes.Enqueue(lectura);
                    nuevas.Add(lectura);
                    TotalGeneradas++;
                }
            }

            _ultimas = nuevas;
        }

        // Pasa las lecturas de la cola al repositorio (en orden de llegada) y escribe el archivo.
        public void Guardar()
        {
            _pasosSinGuardar = 0;
            RepositoryLectura repo = RepositoryLectura.GetInstance();

            while (!_pendientes.IsEmpty)
            {
                Lectura lectura = _pendientes.Dequeue()!;
                try
                {
                    repo.Add(lectura);
                }
                catch (DomainExceptions ex)
                {
                    // Una lectura mala se descarta, pero las demás siguen.
                    Avisar($"Lectura {lectura.Id} descartada: {ex.Message}");
                }
            }

            try
            {
                repo.Commit();
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                // Las lecturas ya están en memoria; el próximo Guardar() lo vuelve a intentar.
                Avisar($"No se pudo escribir lecturas.csv: {ex.Message}");
            }
        }

        // Guarda el aviso en el historial y se lo pasa a quien esté suscrito.
        private void Avisar(string texto)
        {
            _historial.Enqueue(texto);
            if (_historial.Count > MaxHistorial)
            {
                _historial.Dequeue();
            }
            Aviso?.Invoke(texto);
        }

        public double? HumedadDe(long zonaId)
        {
            return _estados.FirstOrDefault(e => e.ZonaId == zonaId)?.Humedad;
        }

        public bool RegandoZona(long zonaId)
        {
            return _estados.FirstOrDefault(e => e.ZonaId == zonaId)?.Regando ?? false;
        }

        // ---------------- Modelo (simplificado) ----------------

        // Ciclo diario: mínimo cerca de las 3 a. m. (≈14 °C) y máximo a las 3 p. m. (≈30 °C).
        private double Temperatura(DateTime hora)
        {
            double h = hora.Hour + hora.Minute / 60.0;
            return 22 + 8 * Math.Sin(2 * Math.PI * (h - 9) / 24);
        }

        // Cada paso hay 3 % de probabilidad de que empiece a llover, entre 30 min y 2 h.
        private double Llover()
        {
            if (_pasosDeLluviaRestantes == 0 && _azar.NextDouble() < 0.03)
            {
                _pasosDeLluviaRestantes = _azar.Next(3, 13);
                _intensidadLluvia = 0.2 + _azar.NextDouble() * 1.8;
                Avisar($"{Reloj:HH:mm} Empezó a llover");
            }

            if (_pasosDeLluviaRestantes == 0)
            {
                return 0;
            }

            _pasosDeLluviaRestantes--;
            if (_pasosDeLluviaRestantes == 0)
            {
                Avisar($"{Reloj:HH:mm} Dejó de llover");
            }
            return _intensidadLluvia;
        }

        // El estado se crea la primera vez que se ve la zona, con humedad algo por encima del mínimo.
        private EstadoZona EstadoDe(ZonaVerde zona)
        {
            EstadoZona? estado = _estados.FirstOrDefault(e => e.ZonaId == zona.Id);
            if (estado == null)
            {
                double inicial = Math.Min(100, zona.HumedadMinima + 10 + _azar.NextDouble() * 20);
                estado = new EstadoZona(zona.Id, inicial);
                _estados.Add(estado);
            }
            return estado;
        }

        // Pierde agua por calor (más si Kc es alto) y gana con la lluvia o el riego.
        // Si baja del mínimo y no llueve, empieza a regar; para al pasar el mínimo + 15 %.
        private void ActualizarHumedad(ZonaVerde zona, EstadoZona estado, double temperatura, double lluvia)
        {
            double perdida = zona.Kc * Math.Max(0.05, 0.03 * (temperatura - 12));
            double ganancia = lluvia * 1.5 + (estado.Regando ? 2.0 : 0);
            estado.Humedad = Math.Clamp(estado.Humedad - perdida + ganancia, 0, 100);

            if (!estado.Regando && estado.Humedad < zona.HumedadMinima && lluvia == 0)
            {
                estado.Regando = true;
                Avisar($"{Reloj:HH:mm} {zona.Nombre}: humedad {estado.Humedad:0.#} % < {zona.HumedadMinima} %, inicia riego");
            }
            else if (estado.Regando && estado.Humedad >= Math.Min(100, zona.HumedadMinima + 15))
            {
                estado.Regando = false;
                Avisar($"{Reloj:HH:mm} {zona.Nombre}: termina riego");
            }
        }

        // Cada sensor lee el valor "real" de su zona con un pequeño error (ruido).
        private double Medir(Sensor sensor, ZonaVerde zona, EstadoZona estado, double temperatura, double lluvia)
        {
            return sensor.Tipo switch
            {
                TipoSensor.HumedadSuelo => Math.Clamp(estado.Humedad + Ruido(1.0), 0, 100),
                TipoSensor.Temperatura => temperatura + Ruido(0.3),
                TipoSensor.Lluvia => lluvia,
                TipoSensor.Caudal => estado.Regando ? CaudalRiego(zona) * (1 + Ruido(0.02)) : 0,
                _ => 0
            };
        }

        // Litros por minuto que entrega el riego: 0,05 L/min por m², al menos 2 L/min.
        public static double CaudalRiego(ZonaVerde zona)
        {
            return Math.Max(2, zona.AreaM2 * 0.05);
        }

        // Número al azar entre -maximo y +maximo.
        private double Ruido(double maximo)
        {
            return (_azar.NextDouble() * 2 - 1) * maximo;
        }
    }
}
