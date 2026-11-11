namespace Proyecto.Calculo
{
    // La humedad como un CAMPO escalar h(x, y): un valor en cada punto de la zona.
    //
    // Solo se conoce la humedad donde hay sensores. Entre ellos se estima con
    // "ponderación por distancia inversa" (IDW): cada sensor pesa más cuanto más cerca está.
    //   h(x, y) = Σ wᵢ · hᵢ / Σ wᵢ      con  wᵢ = 1 / dᵢ²
    // dᵢ es la distancia del punto (x, y) al sensor i. Con un solo sensor el campo es plano.
    //
    // Las coordenadas son las de la zona: de 0 a Ancho en x y de 0 a Largo en y.
    internal class CampoHumedad
    {
        private readonly double[] _x;
        private readonly double[] _y;
        private readonly double[] _h;

        public double Ancho { get; }
        public double Largo { get; }
        public int Sensores => _h.Length;

        public CampoHumedad(double ancho, double largo, IEnumerable<(double x, double y, double h)> puntos)
        {
            Ancho = ancho;
            Largo = largo;
            (double x, double y, double h)[] arreglo = puntos.ToArray();
            _x = arreglo.Select(p => p.x).ToArray();
            _y = arreglo.Select(p => p.y).ToArray();
            _h = arreglo.Select(p => p.h).ToArray();
        }

        public (double x, double y, double h) Sensor(int i)
        {
            return (_x[i], _y[i], _h[i]);
        }

        public double Valor(double x, double y)
        {
            if (_h.Length == 0)
            {
                return 0;
            }

            double suma = 0;
            double pesos = 0;
            for (int i = 0; i < _h.Length; i++)
            {
                double d2 = (x - _x[i]) * (x - _x[i]) + (y - _y[i]) * (y - _y[i]);
                if (d2 < 1e-12)
                {
                    return _h[i];       // justo encima de un sensor: su propio valor
                }
                double w = 1 / d2;
                suma += w * _h[i];
                pesos += w;
            }
            return suma / pesos;
        }

        // Humedad promedio de toda la zona: (1/A) ∬_R h(x, y) dA
        public double Promedio()
        {
            return Integrales.Doble(Valor, 0, Ancho, 0, Largo, 60) / (Ancho * Largo);
        }

        // ∇h = (∂h/∂x, ∂h/∂y) con diferencias centrales. Apunta hacia donde la humedad
        // SUBE más rápido; −∇h apunta hacia donde el terreno se SECA.
        // En los bordes se usa el punto del borde para no salir de la zona.
        public (double dx, double dy) Gradiente(double x, double y)
        {
            double paso = 0.005 * Math.Max(Ancho, Largo);

            double x1 = Math.Max(0, x - paso);
            double x2 = Math.Min(Ancho, x + paso);
            double y1 = Math.Max(0, y - paso);
            double y2 = Math.Min(Largo, y + paso);

            double dx = (Valor(x2, y) - Valor(x1, y)) / (x2 - x1);
            double dy = (Valor(x, y2) - Valor(x, y1)) / (y2 - y1);
            return (dx, dy);
        }
    }
}
