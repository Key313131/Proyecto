using Proyecto.Calculo;
using Proyecto.Domain;
using System.Drawing.Drawing2D;

namespace Proyecto.Design
{
    // Dibuja el campo de humedad h(x, y) de una zona como un "mapa de calor"
    // y, encima, flechas con −∇h: hacia dónde se está secando el terreno.
    public class LienzoCampo : Control
    {
        private const int Margen = 36;
        private const int Celdas = 60;          // cuadritos de color a lo ancho
        private const int Flechas = 8;          // flechas a lo ancho

        private static readonly Color Seco = Color.FromArgb(150, 95, 45);
        private static readonly Color EnMinimo = Color.FromArgb(220, 190, 70);
        private static readonly Color Objetivo = Color.FromArgb(90, 170, 70);
        private static readonly Color Humedo = Color.FromArgb(50, 120, 200);

        private ZonaVerde? _zona;
        private CampoHumedad? _campo;

        public LienzoCampo()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        internal void Mostrar(ZonaVerde? zona, CampoHumedad? campo)
        {
            _zona = zona;
            _campo = campo;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(BackColor);

            if (_zona == null)
            {
                Mensaje(g, "No hay una zona seleccionada");
                return;
            }

            // Escala: metros → píxeles, dejando espacio abajo para la leyenda.
            double escala = Math.Min((Width - 2.0 * Margen) / _zona.AnchoM,
                                     (Height - 2.0 * Margen - 30) / _zona.LargoM);
            if (escala <= 0)
            {
                return;
            }
            float ancho = (float)(_zona.AnchoM * escala);
            float largo = (float)(_zona.LargoM * escala);
            RectangleF area = new RectangleF((Width - ancho) / 2, (Height - 30 - largo) / 2, ancho, largo);

            bool hayCampo = _campo != null && _campo.Sensores > 0;
            if (hayCampo)
            {
                DibujarCalor(g, area);
                DibujarFlechas(g, area, escala);
                DibujarSensores(g, area, escala);
            }
            else
            {
                using SolidBrush verde = new SolidBrush(Objetivo);
                g.FillRectangle(verde, area);
            }

            using (Pen borde = new Pen(Color.White, 1.5f))
            {
                g.DrawRectangle(borde, area.X, area.Y, area.Width, area.Height);
            }

            string leyenda = hayCampo
                ? $"{_zona.Nombre}  ·  café = seco, amarillo = en el mínimo ({_zona.HumedadMinima:0.#} %), "
                  + $"verde = objetivo ({ModeloAgua.HumedadObjetivo(_zona):0.#} %), azul = húmedo  ·  flechas: hacia dónde se seca (−∇h)"
                : $"{_zona.Nombre}  ·  sin lecturas de sensores de humedad: inicie el simulador para ver el campo";
            using SolidBrush texto = new SolidBrush(ForeColor);
            RectangleF caja = new RectangleF(8, area.Bottom + 6, Width - 16, Height - area.Bottom - 6);
            g.DrawString(leyenda, Font, texto, caja);
        }

        // Cada cuadrito se pinta con el color de h en su centro.
        private void DibujarCalor(Graphics g, RectangleF area)
        {
            int nx = Celdas;
            int ny = Math.Clamp((int)Math.Round(Celdas * _zona!.LargoM / _zona.AnchoM), 1, Celdas);
            float w = area.Width / nx;
            float h = area.Height / ny;

            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    double x = (i + 0.5) * _zona.AnchoM / nx;
                    double y = (j + 0.5) * _zona.LargoM / ny;
                    using SolidBrush brocha = new SolidBrush(ColorDe(_campo!.Valor(x, y)));
                    // +1 para que no queden rayitas entre cuadros
                    g.FillRectangle(brocha, area.X + i * w, area.Y + j * h, w + 1, h + 1);
                }
            }
        }

        private void DibujarFlechas(Graphics g, RectangleF area, double escala)
        {
            int nx = Flechas;
            int ny = Math.Clamp((int)Math.Round(Flechas * _zona!.LargoM / _zona.AnchoM), 1, Flechas);
            double pasoX = _zona.AnchoM / nx;
            double pasoY = _zona.LargoM / ny;

            // Primero se busca el gradiente más grande para que la flecha más larga mida
            // casi un espacio entre flechas.
            (double dx, double dy)[,] gradientes = new (double, double)[nx, ny];
            double mayor = 0;
            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    gradientes[i, j] = _campo!.Gradiente((i + 0.5) * pasoX, (j + 0.5) * pasoY);
                    mayor = Math.Max(mayor, Derivadas.Norma(gradientes[i, j].dx, gradientes[i, j].dy));
                }
            }
            if (mayor < 1e-6)
            {
                return;     // campo plano (por ejemplo, un solo sensor): no hay hacia dónde
            }

            double largoMaximo = 0.8 * Math.Min(pasoX, pasoY) * escala;
            using AdjustableArrowCap punta = new AdjustableArrowCap(4, 4);
            using Pen lapiz = new Pen(Color.FromArgb(230, 255, 255, 255), 2f);
            lapiz.CustomEndCap = punta;

            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    (double dx, double dy) = gradientes[i, j];
                    double factor = largoMaximo / mayor;
                    float x0 = area.X + (float)((i + 0.5) * pasoX * escala);
                    float y0 = area.Y + (float)((j + 0.5) * pasoY * escala);
                    // −∇h: hacia donde baja la humedad
                    float x1 = x0 - (float)(dx * factor);
                    float y1 = y0 - (float)(dy * factor);
                    if (Math.Abs(x1 - x0) + Math.Abs(y1 - y0) > 2)
                    {
                        g.DrawLine(lapiz, x0, y0, x1, y1);
                    }
                }
            }
        }

        private void DibujarSensores(Graphics g, RectangleF area, double escala)
        {
            using SolidBrush blanco = new SolidBrush(Color.White);
            using SolidBrush oscuro = new SolidBrush(Tema.Fondo);
            using Pen borde = new Pen(Tema.Fondo, 2);
            for (int i = 0; i < _campo!.Sensores; i++)
            {
                (double x, double y, double h) = _campo.Sensor(i);
                float px = area.X + (float)(x * escala);
                float py = area.Y + (float)(y * escala);
                g.FillEllipse(blanco, px - 6, py - 6, 12, 12);
                g.DrawEllipse(borde, px - 6, py - 6, 12, 12);

                string texto = $"{h:0.#} %";
                SizeF tam = g.MeasureString(texto, Font);
                g.FillRectangle(blanco, px + 8, py - tam.Height / 2, tam.Width, tam.Height);
                g.DrawString(texto, Font, oscuro, px + 8, py - tam.Height / 2);
            }
        }

        // Escala de colores por tramos: se mezcla linealmente entre dos colores vecinos.
        private Color ColorDe(double humedad)
        {
            double minimo = _zona!.HumedadMinima;
            double objetivo = ModeloAgua.HumedadObjetivo(_zona);
            (double valor, Color color)[] tramos =
            {
                (minimo - 10, Seco),
                (minimo, EnMinimo),
                (objetivo, Objetivo),
                (objetivo + 15, Humedo)
            };

            if (humedad <= tramos[0].valor)
            {
                return tramos[0].color;
            }
            for (int i = 1; i < tramos.Length; i++)
            {
                if (humedad <= tramos[i].valor)
                {
                    double t = (humedad - tramos[i - 1].valor) / (tramos[i].valor - tramos[i - 1].valor);
                    return Mezclar(tramos[i - 1].color, tramos[i].color, t);
                }
            }
            return tramos[^1].color;
        }

        private static Color Mezclar(Color a, Color b, double t)
        {
            return Color.FromArgb(
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t));
        }

        private void Mensaje(Graphics g, string mensaje)
        {
            using StringFormat centrado = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            using SolidBrush texto = new SolidBrush(ForeColor);
            g.DrawString(mensaje, Font, texto, ClientRectangle, centrado);
        }
    }
}
