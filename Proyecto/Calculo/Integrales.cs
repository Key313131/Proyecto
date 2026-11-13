namespace Proyecto.Calculo
{
    // Integrales numéricas con la regla de Simpson.
    //
    // Simpson en 1D: se parte [a, b] en n tramos (n par) de ancho h y
    //   ∫ f ≈ h/3 · [f₀ + 4f₁ + 2f₂ + 4f₃ + ... + 4fₙ₋₁ + fₙ]
    // Es exacta para polinomios de grado 3 o menos, y el error baja como h⁴.
    // En 2D se aplica lo mismo en cada dirección: el peso de un punto es el producto
    // de los pesos de cada coordenada.
    internal static class Integrales
    {
        // ∬_R f(x, y) dA  sobre el rectángulo R = [a, b] × [c, d]
        public static double Doble(Func<double, double, double> f, double a, double b, double c, double d, int n = 40)
        {
            n = Par(n);
            double hx = (b - a) / n;
            double hy = (d - c) / n;
            double suma = 0;

            for (int i = 0; i <= n; i++)
            {
                double x = a + i * hx;
                for (int j = 0; j <= n; j++)
                {
                    double y = c + j * hy;
                    suma += Peso(i, n) * Peso(j, n) * f(x, y);
                }
            }
            return suma * hx * hy / 9;
        }

        // FLUJO del agua por la sección circular de un tubo (integral de superficie).
        // El agua va más rápido en el centro que en las paredes (perfil parabólico):
        //   v(r) = vmax · (1 − r² / R²)
        // Caudal = flujo de v a través del círculo:
        //   Q = ∬_S v · n dS = ∫₀^2π ∫₀^R vmax (1 − r²/R²) · r dr dθ = π R² vmax / 2
        // El "· r" es el jacobiano de las coordenadas polares. Devuelve m³/s.
        public static double FlujoTubo(double radio, double vmax, int n = 60)
        {
            return Doble((r, theta) => vmax * (1 - r * r / (radio * radio)) * r,
                0, radio, 0, 2 * Math.PI, n);
        }

        // Peso de Simpson del punto i: 1 en los extremos, 4 en los impares, 2 en los pares.
        private static double Peso(int i, int n)
        {
            if (i == 0 || i == n)
            {
                return 1;
            }
            return i % 2 == 1 ? 4 : 2;
        }

        private static int Par(int n)
        {
            n = Math.Max(2, n);
            return n % 2 == 0 ? n : n + 1;
        }
    }
}
