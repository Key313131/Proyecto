using Proyecto.Domain;

namespace Proyecto.Calculo
{
    // Las variables de las que depende la necesidad de agua.
    // "record struct" crea un tipo pequeño de solo datos con constructor y ToString listos.
    internal readonly record struct VariablesAgua(double Temperatura, double Humedad, double Area, double HorasSol);

    // Modelo de necesidad de agua de una zona: una función de VARIAS variables
    //
    //   W(T, H, A, S) = A · [ Kc · ET₀(T, S)  +  max(0, Hobj − H) / 100 · Pr · 1000 · θ ]
    //                        └ lo que se evapora ┘   └ lo que falta para llegar a Hobj ┘
    //
    //   T  temperatura (°C)            H  humedad del suelo (%)
    //   A  área (m²)                   S  horas de luz del día
    //   Kc coeficiente de cultivo      Pr profundidad de raíz (m)
    //   θ  porosidad: fracción del suelo que puede llenarse de agua
    //   Hobj humedad objetivo = mínima + 15 (la misma regla del simulador)
    //
    // Los dos términos están en mm de agua (lámina). Como 1 mm sobre 1 m² son 1 litro,
    // multiplicar por el área da litros por día.
    //
    // ET₀ (evapotranspiración de referencia) por el método de Blaney-Criddle (FAO):
    //   ET₀ = p · (0,46 · T + 8,13)      con p = S / 4380 · 100
    // p es el % de las horas de luz del año que tiene ese día (4380 h ≈ 12 h × 365).
    internal static class ModeloAgua
    {
        public const double Porosidad = 0.4;
        public const double MargenObjetivo = 15;
        public const double HorasLuzAnuales = 4380;

        public static double HumedadObjetivo(ZonaVerde zona)
        {
            return Math.Min(100, zona.HumedadMinima + MargenObjetivo);
        }

        public static double Et0(double temperatura, double horasSol)
        {
            double p = horasSol / HorasLuzAnuales * 100;
            return Math.Max(0, p * (0.46 * temperatura + 8.13));
        }

        // Lámina (mm) para volver a "humedadObjetivo" con suelo de profundidad "raiz".
        public static double LaminaReposicion(double humedad, double humedadObjetivo, double raiz)
        {
            return Math.Max(0, humedadObjetivo - humedad) / 100 * raiz * 1000 * Porosidad;
        }

        // Litros por día que necesita la zona.
        public static double Necesidad(ZonaVerde zona, VariablesAgua v)
        {
            return Necesidad(zona, v, HumedadObjetivo(zona));
        }

        // Con otra humedad objetivo: con la humedad MÍNIMA da el agua mínima del reparto.
        public static double Necesidad(ZonaVerde zona, VariablesAgua v, double humedadObjetivo)
        {
            double evaporacion = zona.Kc * Et0(v.Temperatura, v.HorasSol);
            double reposicion = LaminaReposicion(v.Humedad, humedadObjetivo, zona.ProfundidadRaizM);
            return v.Area * (evaporacion + reposicion);
        }

        // La misma W pero recibiendo las variables en un arreglo [T, H, A, S],
        // que es lo que necesitan las derivadas numéricas.
        public static Func<double[], double> ComoFuncion(ZonaVerde zona)
        {
            return p => Necesidad(zona, new VariablesAgua(p[0], p[1], p[2], p[3]));
        }

        // Gradiente EXACTO, derivando la fórmula a mano:
        //   ∂W/∂T = A · Kc · p · 0,46                         (más calor → más agua)
        //   ∂W/∂H = −A · Pr · 1000 · θ / 100  si H < Hobj; 0 si no   (más húmedo → menos agua)
        //   ∂W/∂A = Kc · ET₀ + reposición  (la lámina)         (más área → más agua)
        //   ∂W/∂S = A · Kc · (0,46 · T + 8,13) · 100 / 4380    (más luz → más agua)
        public static double[] Gradiente(ZonaVerde zona, VariablesAgua v)
        {
            double p = v.HorasSol / HorasLuzAnuales * 100;
            double hObjetivo = HumedadObjetivo(zona);

            double dT = v.Area * zona.Kc * p * 0.46;
            double dH = v.Humedad < hObjetivo ? -v.Area * zona.ProfundidadRaizM * 1000 * Porosidad / 100 : 0;
            double dA = zona.Kc * Et0(v.Temperatura, v.HorasSol)
                        + LaminaReposicion(v.Humedad, hObjetivo, zona.ProfundidadRaizM);
            double dS = v.Area * zona.Kc * (0.46 * v.Temperatura + 8.13) * 100 / HorasLuzAnuales;

            return new[] { dT, dH, dA, dS };
        }
    }
}
