using Proyecto.Domain;

namespace Proyecto.Calculo
{
    // El terreno de la instalación como una superficie z = f(x, y), en metros.
    // (x, y) son coordenadas GLOBALES: la posición de la zona (X, Y) más la del punto
    // dentro de la zona.
    //
    // Modelo: una loma suave centrada en (CentroX, CentroY) más una inclinación hacia +x.
    //   f(x, y) = AlturaLoma · e^(−[(x − cx)² + (y − cy)²] / (2σ²)) + InclinacionX · x
    internal static class Terreno
    {
        public const double AlturaLoma = 4.0;       // m
        public const double CentroX = 50.0;         // m
        public const double CentroY = 50.0;         // m
        public const double Sigma = 30.0;           // m: qué tan ancha es la loma
        public const double InclinacionX = 0.02;    // 2 cm de subida por cada metro hacia +x

        // Cuánta humedad pierde el suelo por cada metro de altura (el agua escurre hacia abajo).
        public const double HumedadPorMetro = 5.0;  // % por m

        public static double Altura(double x, double y)
        {
            return Loma(x, y) + InclinacionX * x;
        }

        // Gradiente exacto (derivadas parciales calculadas a mano):
        //   ∂f/∂x = loma · (−(x − cx) / σ²) + InclinacionX
        //   ∂f/∂y = loma · (−(y − cy) / σ²)
        public static (double dx, double dy) Gradiente(double x, double y)
        {
            double loma = Loma(x, y);
            double dx = loma * (-(x - CentroX) / (Sigma * Sigma)) + InclinacionX;
            double dy = loma * (-(y - CentroY) / (Sigma * Sigma));
            return (dx, dy);
        }

        // Pendiente en %: cuántos metros sube por cada 100 m en la dirección más empinada.
        // Es la longitud del gradiente: |∇f| = √((∂f/∂x)² + (∂f/∂y)²).
        public static double PendientePorcentaje(double x, double y)
        {
            (double dx, double dy) = Gradiente(x, y);
            return Math.Sqrt(dx * dx + dy * dy) * 100;
        }

        // Diferencia de humedad que causa el terreno en un punto de la zona, comparado con
        // su centro: los puntos más altos quedan más secos. La usa el simulador.
        public static double AjusteHumedad(ZonaVerde zona, double x, double y)
        {
            double centro = Altura(zona.X + zona.AnchoM / 2, zona.Y + zona.LargoM / 2);
            double punto = Altura(zona.X + x, zona.Y + y);
            return -HumedadPorMetro * (punto - centro);
        }

        private static double Loma(double x, double y)
        {
            double distancia2 = (x - CentroX) * (x - CentroX) + (y - CentroY) * (y - CentroY);
            return AlturaLoma * Math.Exp(-distancia2 / (2 * Sigma * Sigma));
        }
    }
}
