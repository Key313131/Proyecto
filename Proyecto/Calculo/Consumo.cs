using Proyecto.Domain;

namespace Proyecto.Calculo
{
    internal readonly record struct ResultadoConsumo(double Litros, int Tramos, int Huecos);

    // Consumo REAL de agua a partir de las lecturas de un medidor de caudal.
    //
    // El medidor da el caudal Q (litros por minuto) en ciertos instantes. El agua que pasó
    // es el área bajo la curva Q(t):
    //   V = ∫ Q(t) dt
    // Como solo hay puntos sueltos, se usa la REGLA DEL TRAPECIO: entre dos lecturas
    // seguidas se supone que el caudal cambia en línea recta, y el área de ese tramo es
    // la de un trapecio:
    //   V ≈ Σ (Qᵢ + Qᵢ₊₁) / 2 · (tᵢ₊₁ − tᵢ)
    //
    // Si entre dos lecturas pasó más de "huecoMaximo" (por ejemplo, el simulador estuvo
    // apagado), ese tramo NO se cuenta: no se sabe qué pasó en ese tiempo.
    internal static class Consumo
    {
        // "ordenadas" debe estar en orden de fecha (las del simulador ya lo están).
        public static ResultadoConsumo Litros(Lectura[] ordenadas, TimeSpan huecoMaximo)
        {
            double litros = 0;
            int tramos = 0;
            int huecos = 0;

            for (int i = 1; i < ordenadas.Length; i++)
            {
                Lectura antes = ordenadas[i - 1];
                Lectura despues = ordenadas[i];
                TimeSpan dt = despues.FechaHora - antes.FechaHora;

                if (dt <= TimeSpan.Zero)
                {
                    continue;       // misma hora: no hay tramo
                }
                if (dt > huecoMaximo)
                {
                    huecos++;
                    continue;
                }

                litros += (antes.Valor + despues.Valor) / 2 * dt.TotalMinutes;
                tramos++;
            }
            return new ResultadoConsumo(litros, tramos, huecos);
        }
    }
}
