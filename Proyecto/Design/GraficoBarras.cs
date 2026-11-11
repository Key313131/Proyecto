using System.Drawing.Drawing2D;

namespace Proyecto.Design
{
    // Gráfico de barras dobles: por cada zona, el consumo real al lado del tradicional.
    public class GraficoBarras : Control
    {
        private const int MargenIzq = 70;
        private const int MargenDer = 16;
        private const int MargenArriba = 34;
        private const int MargenAbajo = 30;

        private static readonly Color ColorReal = Color.FromArgb(70, 170, 90);
        private static readonly Color ColorTradicional = Color.FromArgb(120, 130, 150);

        private (string nombre, double real, double tradicional)[] _datos = Array.Empty<(string, double, double)>();

        public GraficoBarras()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        public void Mostrar(IEnumerable<(string nombre, double real, double tradicional)> datos)
        {
            _datos = datos.ToArray();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(BackColor);

            using SolidBrush texto = new SolidBrush(ForeColor);
            using SolidBrush gris = new SolidBrush(Color.FromArgb(174, 180, 194));

            if (_datos.Length == 0)
            {
                using StringFormat centro = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("No hay zonas con medidor de caudal para graficar", Font, texto, ClientRectangle, centro);
                return;
            }

            Leyenda(g, texto);

            // Escala: la barra más alta llega casi arriba.
            double mayor = _datos.Max(d => Math.Max(d.real, d.tradicional));
            if (mayor <= 0)
            {
                mayor = 1;
            }
            float alto = Height - MargenArriba - MargenAbajo;
            float ancho = Width - MargenIzq - MargenDer;
            float baseY = MargenArriba + alto;

            // Líneas guía: 0, 50 % y 100 % del máximo.
            using Pen guia = new Pen(Tema.Linea, 1);
            using Font chica = new Font(Font.FontFamily, Font.Size * 0.75f);
            for (int i = 0; i <= 2; i++)
            {
                float y = baseY - alto * i / 2;
                g.DrawLine(guia, MargenIzq, y, Width - MargenDer, y);
                g.DrawString($"{mayor * i / 2:N0} L", chica, gris, 4, y - 9);
            }

            float grupo = ancho / _datos.Length;
            float barra = Math.Min(40, grupo * 0.35f);
            using SolidBrush real = new SolidBrush(ColorReal);
            using SolidBrush tradicional = new SolidBrush(ColorTradicional);
            using StringFormat centrado = new StringFormat { Alignment = StringAlignment.Center };

            for (int i = 0; i < _datos.Length; i++)
            {
                (string nombre, double r, double t) = _datos[i];
                float centroX = MargenIzq + grupo * i + grupo / 2;
                float hReal = (float)(r / mayor * alto);
                float hTrad = (float)(t / mayor * alto);

                g.FillRectangle(real, centroX - barra - 2, baseY - hReal, barra, hReal);
                g.FillRectangle(tradicional, centroX + 2, baseY - hTrad, barra, hTrad);
                g.DrawString(nombre, chica, texto, new RectangleF(centroX - grupo / 2, baseY + 4, grupo, MargenAbajo), centrado);
            }
        }

        private void Leyenda(Graphics g, Brush texto)
        {
            using SolidBrush real = new SolidBrush(ColorReal);
            using SolidBrush tradicional = new SolidBrush(ColorTradicional);
            g.FillRectangle(real, MargenIzq, 10, 14, 14);
            g.DrawString("Consumo real", Font, texto, MargenIzq + 18, 6);
            g.FillRectangle(tradicional, MargenIzq + 150, 10, 14, 14);
            g.DrawString("Riego tradicional (lámina fija)", Font, texto, MargenIzq + 168, 6);
        }
    }
}
