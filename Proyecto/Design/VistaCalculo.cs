using Proyecto.Calculo;
using Proyecto.Domain;
using Proyecto.DynamicStructure;
using Proyecto.Infraestructure;
using Proyecto.Presentacion;
using Proyecto.Simulation;

namespace Proyecto.Design
{
    // Pantalla del módulo matemático. Tiene dos resultados:
    //  - "Calcular zona": W(T, H, A, S), sus derivadas, el campo h(x, y) y su gradiente,
    //    el terreno, las integrales y el flujo, para la zona elegida.
    //  - "Repartir": reparte el presupuesto diario entre todas las zonas con Lagrange.
    // Los cálculos tardan milisegundos, por eso no hace falta Task.Run aquí.
    public partial class VistaCalculo : UserControl
    {
        private bool _cargando;

        public VistaCalculo()
        {
            InitializeComponent();

            // Carga lecturas.csv ya: si está dañado, Principal lo avisa al abrir la vista.
            RepositoryLectura.GetInstance();

            Tema.Tabla(dgvReparto);
            dgvReparto.Columns.Add(Tema.Columna("Zona", "Zona", 0));
            dgvReparto.Columns.Add(Tema.Columna("Kc", "Kc", 50));
            dgvReparto.Columns.Add(Tema.Columna("Humedad", "Humedad", 85));
            dgvReparto.Columns.Add(Tema.Columna("Necesidad", "Necesita (L)", 110));
            dgvReparto.Columns.Add(Tema.Columna("Minimo", "Mínimo (L)", 100));
            dgvReparto.Columns.Add(Tema.Columna("Asignado", "Asignado (L)", 110));
            dgvReparto.Columns.Add(Tema.Columna("Cubierto", "Cubierto", 85));
            dgvReparto.Columns.Add(Tema.Columna("Costo", "Costo ($)", 95));

            _cargando = true;
            var zonas = ZonasActivas()
                .Select(z => new { z.Id, Texto = $"{z.Id} - {z.Nombre}" })
                .ToList();
            cmbZona.DataSource = zonas;
            _cargando = false;

            if (zonas.Count == 0)
            {
                btnTomar.Enabled = false;
                btnCalcular.Enabled = false;
                btnRepartir.Enabled = false;
                lienzo.Mostrar(null, null);
                lblEstado.Text = "No hay zonas activas: cree una en Zonas verdes";
                return;
            }

            TomarLecturas();
            CalcularZona();

            // Presupuesto inicial: el 70 % de lo que costaría darle a cada zona todo lo que pide.
            double litros = Necesidades().Sum(z => z.Necesidad);
            double costo = litros / 1000 * (double)nudTarifa.Value;
            UiHelper.Poner(nudPresupuesto, Math.Round(costo * 0.7 / 100) * 100);
        }

        private static IEnumerable<ZonaVerde> ZonasActivas()
        {
            return RepositoryZona.GetInstance().GetAll().Where(z => z.Activo);
        }

        private ZonaVerde? ZonaSeleccionada()
        {
            return cmbZona.SelectedValue is long id ? RepositoryZona.GetInstance().GetById(id) : null;
        }

        private VariablesAgua Variables(ZonaVerde zona, double humedad)
        {
            return new VariablesAgua((double)nudTemp.Value, humedad, zona.AreaM2, (double)nudSol.Value);
        }

        private void cmbZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando)
            {
                return;
            }
            TomarLecturas();
            CalcularZona();
        }

        private void btnTomar_Click(object sender, EventArgs e)
        {
            TomarLecturas();
            CalcularZona();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            CalcularZona();
        }

        private void btnRepartir_Click(object sender, EventArgs e)
        {
            Repartir();
        }

        // Pone en los controles la temperatura y la humedad de las últimas lecturas de la zona.
        private void TomarLecturas()
        {
            ZonaVerde? zona = ZonaSeleccionada();
            if (zona == null)
            {
                return;
            }

            double? temperatura = TemperaturaDe(zona);
            if (temperatura != null)
            {
                UiHelper.Poner(nudTemp, temperatura.Value);
            }
            UiHelper.Poner(nudHum, HumedadDe(zona));
        }

        private void CalcularZona()
        {
            ZonaVerde? zona = ZonaSeleccionada();
            if (zona == null)
            {
                return;
            }

            CampoHumedad campo = CampoDe(zona);
            ResultadoZona r = AnalisisZona.Calcular(zona, Variables(zona, (double)nudHum.Value), campo);

            lienzo.Mostrar(zona, campo);
            lstZona.Items.Clear();
            foreach (string linea in r.Detalle)
            {
                lstZona.Items.Add(linea);
            }

            panelReparto.Visible = false;
            panelZona.Visible = true;
            lblEstado.Text = $"{zona.Nombre}: necesita {r.Necesidad:N0} L hoy ({r.Lamina:0.##} mm)";
        }

        // Una fila por zona activa con lo que necesita (llegar a la humedad objetivo)
        // y lo mínimo (llegar a la humedad mínima), con la misma T y S para todas.
        private Lista<ZonaReparto> Necesidades()
        {
            Lista<ZonaReparto> zonas = new Lista<ZonaReparto>();
            foreach (ZonaVerde zona in ZonasActivas())
            {
                double humedad = HumedadDe(zona);
                VariablesAgua v = Variables(zona, humedad);
                double necesidad = ModeloAgua.Necesidad(zona, v);
                double minimo = ModeloAgua.Necesidad(zona, v, zona.HumedadMinima);
                zonas.Add(new ZonaReparto(zona, humedad, necesidad, minimo));
            }
            return zonas;
        }

        private void Repartir()
        {
            double tarifa = (double)nudTarifa.Value;                // $ por m³
            double litros = (double)nudPresupuesto.Value / tarifa * 1000;
            ResultadoReparto r = RepartoLagrange.Repartir(Necesidades(), litros);

            var filas = r.Zonas.Select(z => new
            {
                Zona = $"{z.Zona.Id} - {z.Zona.Nombre}",
                z.Zona.Kc,
                Humedad = $"{z.Humedad:0.#} %",
                Necesidad = z.Necesidad.ToString("N0"),
                Minimo = z.Minimo.ToString("N0"),
                Asignado = z.Asignado.ToString("N0") + (z.EnMinimo ? " (mín.)" : ""),
                Cubierto = $"{z.PorcentajeCubierto:0} %",
                Costo = (z.Asignado / 1000 * tarifa).ToString("N0")
            }).ToList();
            dgvReparto.DataSource = filas;

            lstReparto.Items.Clear();
            lstReparto.Items.Add($"Presupuesto {nudPresupuesto.Value:N0} $ a {tarifa:N0} $/m³ = {litros:N0} L. "
                + $"Necesidad total {r.TotalNecesidad:N0} L, mínimo total {r.TotalMinimo:N0} L.");
            lstReparto.Items.Add("min D = Σ pᵢ(Wᵢ − xᵢ)²  con  Σ xᵢ = B  y  xᵢ ≥ Mᵢ,   pᵢ = Kcᵢ / Aᵢ   →   xᵢ = Wᵢ − λ / (2pᵢ)");
            foreach (string linea in r.Detalle)
            {
                lstReparto.Items.Add(linea);
            }
            if (r.Factible)
            {
                lstReparto.Items.Add($"Daño D = {RepartoLagrange.Dano(r.Zonas):0.####}. Se reparten {r.TotalAsignado:N0} L "
                    + $"({r.TotalAsignado / 1000 * tarifa:N0} $).");
            }

            panelZona.Visible = false;
            panelReparto.Visible = true;
            lblEstado.Text = r.Factible ? "Reparto calculado con multiplicadores de Lagrange" : "El presupuesto no alcanza para la humedad mínima";
        }

        // ---------------- Datos de los sensores ----------------

        // Campo h(x, y) con la última lectura de cada sensor de humedad activo de la zona.
        private static CampoHumedad CampoDe(ZonaVerde zona)
        {
            RepositoryLectura lecturas = RepositoryLectura.GetInstance();
            // List y no nuestra Lista: Lista<T> exige "where T : class" y una tupla es un struct.
            List<(double x, double y, double h)> lista = new List<(double x, double y, double h)>();

            foreach (Sensor s in SensoresDe(zona, TipoSensor.HumedadSuelo))
            {
                Lectura? ultima = lecturas.UltimaDe(s.Id);
                if (ultima != null)
                {
                    lista.Add((s.x, s.y, ultima.Valor));
                }
            }
            return new CampoHumedad(zona.AnchoM, zona.LargoM, lista);
        }

        private static double? TemperaturaDe(ZonaVerde zona)
        {
            RepositoryLectura lecturas = RepositoryLectura.GetInstance();
            foreach (Sensor s in SensoresDe(zona, TipoSensor.Temperatura))
            {
                Lectura? ultima = lecturas.UltimaDe(s.Id);
                if (ultima != null)
                {
                    return ultima.Valor;
                }
            }
            return null;
        }

        // Humedad promedio de la zona: con sus sensores si los hay; si no, la del simulador;
        // si tampoco, la mínima de la zona.
        private static double HumedadDe(ZonaVerde zona)
        {
            CampoHumedad campo = CampoDe(zona);
            if (campo.Sensores > 0)
            {
                return campo.Promedio();
            }
            return SimuladorSensores.GetInstance().HumedadDe(zona.Id) ?? zona.HumedadMinima;
        }

        private static IEnumerable<Sensor> SensoresDe(ZonaVerde zona, TipoSensor tipo)
        {
            return RepositorySensor.GetInstance().GetByZona(zona.Id).Where(s => s.Activo && s.Tipo == tipo);
        }
    }
}
