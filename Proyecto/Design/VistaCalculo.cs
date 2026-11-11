using Proyecto.Calculo;
using Proyecto.Presentacion;
using Proyecto.Servicios;

namespace Proyecto.Design
{
    // Plan de riego del día: qué zonas regar, cuánto, a qué hora y cuánto cuesta.
    //
    // Al usuario se le muestra solo lo esencial. El "por qué" de cada número (el modelo
    // W, la integral de flujo, Lagrange) está en el botón "Ver cálculo detallado".
    public partial class VistaCalculo : UserControl
    {
        private readonly ServicioRiego _riego;
        private PlanRiego? _plan;

        public VistaCalculo()
        {
            InitializeComponent();
            _riego = new ServicioRiego();

            Tema.Tabla(dgvPlan);
            dgvPlan.Columns.Add(Tema.Columna("Zona", "Zona", 0));
            dgvPlan.Columns.Add(Tema.Columna("Regar", "¿Regar?", 260));
            dgvPlan.Columns.Add(Tema.Columna("Litros", "Litros", 90));
            dgvPlan.Columns.Add(Tema.Columna("Cubierto", "Cubierto", 85));
            dgvPlan.Columns.Add(Tema.Columna("Horario", "Horario", 120));
            dgvPlan.Columns.Add(Tema.Columna("Costo", "Costo ($)", 95));

            dtpInicio.Value = DateTime.Today.AddHours(5);

            if (!_riego.HayZonasActivas())
            {
                btnCalcular.Enabled = false;
                btnDetalle.Enabled = false;
                lblEstado.Text = "No hay zonas activas: cree una en Zonas verdes";
                return;
            }

            double? temperatura = _riego.TemperaturaActual();
            if (temperatura != null)
            {
                UiHelper.Poner(nudTemp, temperatura.Value);
            }

            // Presupuesto sugerido: lo que cuesta darle a cada zona todo lo que necesita.
            double completo = _riego.CostoCompleto(Parametros());
            UiHelper.Poner(nudPresupuesto, Math.Ceiling(completo / 1000) * 1000);

            Calcular();
        }

        private ParametrosRiego Parametros()
        {
            return new ParametrosRiego
            {
                Temperatura = (double)nudTemp.Value,
                HorasSol = (double)nudSol.Value,
                Presupuesto = (double)nudPresupuesto.Value,
                Tarifa = (double)nudTarifa.Value,
                Inicio = dtpInicio.Value.TimeOfDay,
                HorasDisponibles = (double)nudHoras.Value
            };
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Calcular();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (_plan == null)
            {
                return;
            }
            using FrmDetalleCalculo detalle = new FrmDetalleCalculo(_plan.Detalle);
            detalle.ShowDialog(this);
        }

        private void Calcular()
        {
            _plan = _riego.Planificar(Parametros());

            dgvPlan.DataSource = _plan.Zonas.Select(f => new
            {
                Zona = $"{f.Zona.Id} - {f.Zona.Nombre}",
                Regar = f.Motivo,
                Litros = f.Regar ? f.Litros.ToString("N0") + (f.EnMinimo ? " (mín.)" : "") : "—",
                Cubierto = f.Regar ? $"{f.PorcentajeCubierto:0} %" : "—",
                Horario = f.Desde == null ? "—" : $"{PlanificadorRiego.Hora(f.Desde.Value)} – {PlanificadorRiego.Hora(f.Hasta!.Value)}",
                Costo = f.Regar ? f.Costo.ToString("N0") : "—"
            }).ToList();

            // Un Label (a diferencia de un ListBox) parte las líneas largas para que quepan.
            lblResumen.Text = string.Join(Environment.NewLine + Environment.NewLine, _plan.Resumen);

            lblEstado.Text = _plan.Factible ? "Plan calculado" : "El presupuesto o el agua no alcanzan para la humedad mínima";
        }
    }
}
