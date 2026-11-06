using Proyecto.Domain;
using System.Drawing.Drawing2D;

namespace Proyecto.Design
{
    // Dibuja una zona verde a escala, con sus sensores en la posición X, Y de cada uno.
    public class LienzoZona : Control
    {
        private const int Margen = 44;
        private const int PasoPuntos = 22;

        private static readonly Color VerdeZona = Color.FromArgb(136, 178, 72);
        private static readonly Color GrisZona = Color.FromArgb(82, 96, 92);
        private static readonly Color RojoSensor = Color.FromArgb(226, 28, 28);
        private static readonly Color NaranjaOndas = Color.FromArgb(214, 72, 24);
        private static readonly Color GrisSensor = Color.FromArgb(120, 130, 140);

        private ZonaVerde? _zona;
        private List<Sensor> _sensores = new List<Sensor>();
        private long? _resaltado;

        public LienzoZona()
        {
            // Evita el parpadeo y vuelve a dibujar cuando cambia el tamaño.
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        internal void Mostrar(ZonaVerde? zona, IEnumerable<Sensor> sensores, long? resaltado = null)
        {
            _zona = zona;
            _sensores = sensores.ToList();
            _resaltado = resaltado;
            Invalidate(); // pide a Windows que llame a OnPaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(BackColor);
            DibujarPuntos(g);

            if (_zona == null)
            {
                DibujarMensaje(g, "No hay una zona seleccionada");
                return;
            }

            // La misma escala en los dos ejes para que la zona no se deforme.
            double escala = Math.Min((Width - 2.0 * Margen) / _zona.AnchoM,
                                     (Height - 2.0 * Margen) / _zona.LargoM);
            float ancho = (float)(_zona.AnchoM * escala);
            float largo = (float)(_zona.LargoM * escala);
            RectangleF area = new RectangleF((Width - ancho) / 2, (Height - largo) / 2, ancho, largo);

            DibujarZona(g, area);

            foreach (Sensor sensor in _sensores)
            {
                float x = area.X + (float)(sensor.x * escala);
                float y = area.Y + (float)(sensor.y * escala);
                DibujarSensor(g, sensor, x, y);
            }
        }

        private void DibujarPuntos(Graphics g)
        {
            using SolidBrush punto = new SolidBrush(Tema.Linea);
            for (int x = PasoPuntos; x < Width; x += PasoPuntos)
            {
                for (int y = PasoPuntos; y < Height; y += PasoPuntos)
                {
                    g.FillRectangle(punto, x, y, 2, 2);
                }
            }
        }

        private void DibujarMensaje(Graphics g, string mensaje)
        {
            using StringFormat centrado = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            using SolidBrush texto = new SolidBrush(ForeColor);
            g.DrawString(mensaje, Font, texto, ClientRectangle, centrado);
        }

        private void DibujarZona(Graphics g, RectangleF area)
        {
            using GraphicsPath forma = Redondeado(area, Math.Min(34f, Math.Min(area.Width, area.Height) / 4));
            using SolidBrush relleno = new SolidBrush(_zona!.Activo ? VerdeZona : GrisZona);
            g.FillPath(relleno, forma);

            string estado = _zona.Activo ? "" : " (inactiva)";
            string texto = $"{_zona.Nombre}{estado}  ·  {_zona.AnchoM:0.##} x {_zona.LargoM:0.##} m  ·  {_zona.AreaM2:0.##} m²";
            using SolidBrush brocha = new SolidBrush(ForeColor);
            g.DrawString(texto, Font, brocha, area.X, area.Bottom + 8);
        }

        private void DibujarSensor(Graphics g, Sensor sensor, float x, float y)
        {
            Color cabeza = sensor.Activo ? RojoSensor : GrisSensor;
            Color ondas = sensor.Activo ? NaranjaOndas : GrisSensor;

            if (sensor.Id == _resaltado)
            {
                using Pen anillo = new Pen(Color.White, 2);
                g.DrawEllipse(anillo, x - 26, y - 22, 52, 52);
            }

            // Tres ondas que salen hacia abajo desde la cabeza del sensor.
            using (Pen lapiz = new Pen(ondas, 2.5f))
            {
                for (int radio = 9; radio <= 21; radio += 6)
                {
                    g.DrawArc(lapiz, x - radio, y - radio, radio * 2, radio * 2, 35, 110);
                }
            }

            using (SolidBrush brocha = new SolidBrush(cabeza))
            {
                g.FillRectangle(brocha, x - 8, y - 9, 16, 11);
            }
            using (SolidBrush centro = new SolidBrush(Color.White))
            {
                g.FillEllipse(centro, x - 3, y - 6, 6, 6);
            }

            using SolidBrush texto = new SolidBrush(Tema.Fondo);
            g.DrawString(sensor.Id.ToString(), Font, texto, x + 12, y - 16);
        }

        private static GraphicsPath Redondeado(RectangleF r, float radio)
        {
            float d = radio * 2;
            GraphicsPath forma = new GraphicsPath();
            forma.AddArc(r.X, r.Y, d, d, 180, 90);
            forma.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            forma.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            forma.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            forma.CloseFigure();
            return forma;
        }
    }
}
