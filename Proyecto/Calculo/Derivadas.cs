namespace Proyecto.Calculo
{
    // Derivadas parciales NUMÉRICAS. Sirven para comprobar las derivadas hechas a mano
    // y para funciones que no tienen fórmula (como el campo de humedad interpolado).
    internal static class Derivadas
    {
        // Diferencia central: ∂f/∂xᵢ ≈ [f(..., xᵢ + h, ...) − f(..., xᵢ − h, ...)] / 2h
        // El error es proporcional a h² (mucho mejor que [f(x + h) − f(x)] / h, que es ∝ h).
        // "punto" es un arreglo con el valor de cada variable; "variable" dice cuál se mueve.
        public static double Parcial(Func<double[], double> f, double[] punto, int variable)
        {
            double h = 1e-4 * Math.Max(1, Math.Abs(punto[variable]));

            double[] adelante = (double[])punto.Clone();
            double[] atras = (double[])punto.Clone();
            adelante[variable] += h;
            atras[variable] -= h;

            return (f(adelante) - f(atras)) / (2 * h);
        }

        // Gradiente: el vector con todas las derivadas parciales.
        // ∇f = (∂f/∂x₁, ∂f/∂x₂, ..., ∂f/∂xₙ)
        public static double[] Gradiente(Func<double[], double> f, double[] punto)
        {
            double[] gradiente = new double[punto.Length];
            for (int i = 0; i < punto.Length; i++)
            {
                gradiente[i] = Parcial(f, punto, i);
            }
            return gradiente;
        }

        public static double Norma(double dx, double dy)
        {
            return Math.Sqrt(dx * dx + dy * dy);
        }

        // Dirección de un vector en el plano de la zona, con x hacia la derecha (este)
        // y y hacia abajo (sur), como se dibuja en pantalla.
        public static string Direccion(double dx, double dy)
        {
            if (Norma(dx, dy) < 1e-9)
            {
                return "ninguna (plano)";
            }

            // Ángulo medido desde el este, en sentido antihorario; y se invierte porque en
            // pantalla crece hacia abajo.
            double grados = Math.Atan2(-dy, dx) * 180 / Math.PI;
            if (grados < 0)
            {
                grados += 360;
            }

            string[] nombres = { "este", "noreste", "norte", "noroeste", "oeste", "suroeste", "sur", "sureste" };
            int sector = (int)Math.Round(grados / 45) % 8;
            return nombres[sector];
        }
    }
}
