using Proyecto.Calculo;
using Proyecto.Servicios;

namespace Proyecto.Design
{
    // Dashboard: cuánta agua se gastó de verdad, cuánto costó, qué tan eficiente fue
    // (L/m²) y cuánto se ahorró frente a regar siempre lo mismo (riego tradicional).
    public partial class VistaTablero : UserControl
    {
        private readonly ServicioTablero _tablero;
        private Tablero? _resultado;
        private bool _cargando;

        public VistaTablero()
        {
            InitializeComponent();
            _tablero = new ServicioTablero();

            Tema.Tabla(dgvTablero);
            dgvTablero.Columns.Add(Tema.Columna("Zona", "Zona", 0));
            dgvTablero.Columns.Add(Tema.Columna("Area", "Área (m²)", 90));
            dgvTablero.Columns.Add(Tema.Columna("Consumo", "Consumo (L)", 105));
            dgvTablero.Columns.Add(Tema.Columna("Eficiencia", "L/m²", 70));
            dgvTablero.Columns.Add(Tema.Columna("Costo", "Costo ($)", 95));
            dgvTablero.Columns.Add(Tema.Columna("Tradicional", "Tradicional (L)", 120));
            dgvTablero.Columns.Add(Tema.Columna("Ahorro", "Ahorro", 80));
            dgvTablero.Columns.Add(Tema.Columna("Meta", "Meta", 70));

            _cargando = true;
            cmbPeriodo.DataSource = Enum.GetValues<PeriodoTablero>()
                .Select(p => new { Valor = p, Texto = ServicioTablero.Texto(p) })
                .ToList();
            cmbOrden.DataSource = Enum.GetValues<CriterioTablero>();

            // Una semana es más representativa que un día: un riego dura varias horas.
            cmbPeriodo.SelectedValue = PeriodoTablero.UltimaSemana;
            _cargando = false;

            if (!_tablero.HayLecturas())
            {
                lblEstado.Text = "Aún no hay lecturas: inicie el simulador para generar consumo";
            }
            Actualizar();
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_cargando)
            {
                Actualizar();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            if (cmbPeriodo.SelectedValue is not PeriodoTablero periodo)
            {
                return;
            }

            _resultado = _tablero.Calcular(periodo, (double)nudTarifa.Value, (double)nudLamina.Value,
                (double)nudMeta.Value, (CriterioTablero)cmbOrden.SelectedItem!, chkDescendente.Checked);
            Tablero t = _resultado;

            lblConsumo.Text = $"Consumo\n{t.Litros:N0} L";
            lblCosto.Text = $"Costo\n{t.Costo:N0} $";
            lblEficiencia.Text = $"Eficiencia\n{t.LitrosPorM2:0.##} L/m²";
            lblAhorro.Text = t.Tradicional > 0 ? $"Ahorro\n{t.Ahorro:0} %" : "Ahorro\n—";
            lblAhorro.BackColor = t.Tradicional <= 0 ? Tema.Tarjeta
                : t.CumpleMeta ? Color.FromArgb(40, 120, 60) : Color.FromArgb(150, 60, 50);

            grafico.Mostrar(t.Zonas.Where(f => f.TieneMedidor).Select(f => (f.Zona.Nombre, f.Litros, f.Tradicional)));

            dgvTablero.DataSource = t.Zonas.Select(f => new
            {
                Zona = $"{f.Zona.Id} - {f.Zona.Nombre}",
                Area = f.Zona.AreaM2.ToString("N1"),
                Consumo = f.TieneMedidor ? f.Litros.ToString("N0") : "sin medidor",
                Eficiencia = f.TieneMedidor ? f.LitrosPorM2.ToString("0.##") : "—",
                Costo = f.TieneMedidor ? f.Costo.ToString("N0") : "—",
                Tradicional = f.TieneMedidor ? f.Tradicional.ToString("N0") : "—",
                Ahorro = f.TieneMedidor && f.Tradicional > 0 ? $"{f.Ahorro:0} %" : "—",
                Meta = !f.TieneMedidor || f.Tradicional <= 0 ? "—" : f.CumpleMeta ? "Cumple" : "No"
            }).ToList();

            string sinMedidor = t.ZonasSinMedidor > 0 ? $" · {t.ZonasSinMedidor} zona(s) sin medidor de caudal" : "";
            lblEstado.Text = $"{t.Desde:dd/MM HH:mm} a {t.Hasta:dd/MM HH:mm} ({t.Dias:0.#} días) · "
                + $"meta de ahorro {nudMeta.Value:0} %: {(t.CumpleMeta ? "se cumple" : "no se cumple")}{sinMedidor}";
        }
    }
}
