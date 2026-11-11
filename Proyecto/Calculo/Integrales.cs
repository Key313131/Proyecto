namespace Proyecto.Calculo
{
    // Integrales numéricas con la regla de Simpson.
    //
    // Simpson en 1D: se parte [a, b] en n tramos (n par) de ancho h y
    //   ∫ f ≈ h/3 · [f₀ + 4f₁ + 2f₂ + 4f₃ + ... + 4fₙ₋₁ + fₙ]
    // Es exacta para polinomios de grado 3 o menos, y el error baja como h⁴.
    // En 2D y 3D se aplica lo mismo en cada dirección: el peso de un punto es el
    // producto de los pesos de cada coordenada.
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

        // ∭_V f(x, y, z) dV  sobre la caja V = [a, b] × [c, d] × [e, g]
        public static double Triple(Func<double, double, double, double> f,
            double a, double b, double c, double d, double e, double g, int n = 20)
        {
            n = Par(n);
            double hx = (b - a) / n;
            double hy = (d - c) / n;
            double hz = (g - e) / n;
            double suma = 0;

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    for (int k = 0; k <= n; k++)
                    {
                        double peso = Peso(i, n) * Peso(j, n) * Peso(k, n);
                        suma += peso * f(a + i * hx, c + j * hy, e + k * hz);
                    }
                }
            }
            return suma * hx * hy * hz / 27;
        }

        // Área de un polígono con el TEOREMA DE GREEN:
        //   A = ∬_R 1 dA = ½ ∮_C (x dy − y dx)
        // En un polígono, la integral de línea sobre cada lado da la fórmula "del zapato":
        //   A = ½ |Σ (xᵢ · yᵢ₊₁ − xᵢ₊₁ · yᵢ)|
        // Sirve para zonas irregulares: solo hacen falta las esquinas, en orden.
        public static double AreaGreen(IReadOnlyList<(double x, double y)> vertices)
        {
            double suma = 0;
            for (int i = 0; i < vertices.Count; i++)
            {
                (double x1, double y1) = vertices[i];
                (double x2, double y2) = vertices[(i + 1) % vertices.Count];   // el último se une al primero
                suma += x1 * y2 - x2 * y1;
            }
            return Math.Abs(suma) / 2;
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

        // Velocidad en el centro del tubo para que pase el caudal Q (m³/s): vmax = 2Q / (πR²)
        public static double VelocidadMaxima(double caudal, double radio)
        {
            return 2 * caudal / (Math.PI * radio * radio);
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
