using Proyecto.Algorithms;
using Proyecto.Presentacion;
using Proyecto.Servicios;

namespace Proyecto.Design
{
    // Ventana técnica (para Estructura de Datos): cómo se resolvió la última consulta,
    // la complejidad de cada algoritmo y una comparación de los cuatro con n datos.
    public partial class FrmAnalisisAlgoritmos : Form
    {
        private readonly ServicioLecturas _lecturas = new ServicioLecturas();

        internal FrmAnalisisAlgoritmos(IEnumerable<string> ultimaConsulta)
        {
            InitializeComponent();
            lstDetalle.Items.Add("ÚLTIMA CONSULTA, PASO A PASO");
            if (!ultimaConsulta.Any())
            {
                lstDetalle.Items.Add("   (todavía no se ha hecho ninguna consulta)");
            }
            foreach (string linea in ultimaConsulta)
            {
                lstDetalle.Items.Add("   " + linea);
            }

            lstDetalle.Items.Add("");
            lstDetalle.Items.Add("COMPLEJIDAD");
            foreach (Algoritmo algoritmo in Enum.GetValues<Algoritmo>())
            {
                string estable = Ordenamiento.EsEstable(algoritmo) ? "estable" : "no estable";
                string usado = algoritmo == Algoritmo.MergeSort ? "  ← el que usa el programa" : "";
                lstDetalle.Items.Add($"   {algoritmo}: {Ordenamiento.BigO(algoritmo)}, {estable}{usado}");
            }
            lstDetalle.Items.Add("   Búsqueda lineal O(n); búsqueda binaria O(log n), requiere datos ordenados");
        }

        private async void btnComparar_Click(object sender, EventArgs e)
        {
            int n = (int)nudN.Value;
            lstDetalle.Items.Add("");
            lstDetalle.Items.Add($"COMPARACIÓN CON n = {n:N0}");
            btnComparar.Enabled = false;
            btnComparar.BackColor = Tema.Linea;
            lblEstado.Text = $"Comparando con n = {n:N0}... (la ventana sigue respondiendo)";

            // Progress<T> se crea aquí, en el hilo de la interfaz: cuando el otro hilo llama
            // a Report(...), el texto llega de vuelta a ESTE hilo y se puede tocar la lista.
            Progress<string> progreso = new Progress<string>(linea =>
            {
                if (!IsDisposed)
                {
                    lstDetalle.Items.Add("   " + linea);
                    lstDetalle.TopIndex = lstDetalle.Items.Count - 1;
                }
            });

            try
            {
                await _lecturas.CompararAlgoritmosAsync(n, progreso);
                if (IsDisposed)
                {
                    return;
                }
                lstDetalle.Items.Add($"   Interfaz: hilo #{Environment.CurrentManagedThreadId}");
                lblEstado.Text = $"Comparación terminada con n = {n:N0}";
            }
            catch (Exception ex)
            {
                if (IsDisposed)
                {
                    return;
                }
                lblEstado.Text = "La comparación falló";
                UiHelper.Error(ex.Message);
            }

            btnComparar.Enabled = true;
            btnComparar.BackColor = Tema.Azul;
        }
    }
}
