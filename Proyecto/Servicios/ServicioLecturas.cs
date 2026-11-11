using Proyecto.Algorithms;
using Proyecto.Domain;
using Proyecto.DynamicStructure;
using Proyecto.Infraestructure;

namespace Proyecto.Servicios
{
    // Consultas sobre el historial de lecturas.
    //
    // Los métodos "Async" hacen el trabajo pesado con Task.Run en OTRO hilo. Antes de
    // eso copian las lecturas a un arreglo, aquí mismo, en el hilo de quien llama (la
    // interfaz). El otro hilo solo usa esa copia, nunca la Lista del repositorio, que el
    // simulador puede estar modificando al mismo tiempo.
    internal class ServicioLecturas
    {
        private readonly RepositoryLectura _lecturas;

        public ServicioLecturas()
        {
            _lecturas = RepositoryLectura.GetInstance();
        }

        public int Total()
        {
            return _lecturas.GetAll().Count();
        }

        public DateTime? UltimaFecha()
        {
            return _lecturas.UltimaFecha();
        }

        public Lectura? UltimaDe(long sensorId)
        {
            return _lecturas.UltimaDe(sensorId);
        }

        public Task<ResultadoConsulta> ConsultarAsync(FiltroConsulta filtro)
        {
            Lectura[] copia = Copiar();
            return Task.Run(() => ConsultaLecturas.Ejecutar(copia, filtro));
        }

        public Task<ResultadoConsulta> BuscarPorIdAsync(long id)
        {
            Lectura[] copia = Copiar();
            return Task.Run(() => ConsultaLecturas.BuscarPorId(copia, id));
        }

        // No usa lecturas guardadas: inventa n datos, así que no hace falta copiar nada.
        public Task<Lista<FilaComparacion>> CompararAlgoritmosAsync(int n, IProgress<string>? progreso)
        {
            return Task.Run(() => ConsultaLecturas.Comparar(n, progreso));
        }

        private Lectura[] Copiar()
        {
            return _lecturas.GetAll().ToArray();
        }
    }
}
