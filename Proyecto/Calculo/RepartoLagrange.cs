using Proyecto.Domain;
using Proyecto.DynamicStructure;

namespace Proyecto.Calculo
{
    internal class ZonaReparto
    {
        public ZonaVerde Zona { get; }
        public double Humedad { get; }
        public double Necesidad { get; }    // Wᵢ: lo ideal (llegar a la humedad objetivo)
        public double Minimo { get; }       // Mᵢ: lo mínimo (llegar a la humedad mínima)
        public double Asignado { get; set; }
        public bool EnMinimo { get; set; }  // true si quedó en el mínimo (restricción activa)

        // Peso del déficit: pᵢ = Kcᵢ / Aᵢ. Ver la explicación en RepartoLagrange.
        public double Peso => Zona.Kc / Zona.AreaM2;
        public double PorcentajeCubierto => Necesidad <= 0 ? 100 : Asignado / Necesidad * 100;

        public ZonaReparto(ZonaVerde zona, double humedad, double necesidad, double minimo)
        {
            Zona = zona;
            Humedad = humedad;
            Necesidad = necesidad;
            Minimo = minimo;
        }
    }

    internal class ResultadoReparto
    {
        public Lista<ZonaReparto> Zonas { get; } = new Lista<ZonaReparto>();
        public Lista<string> Detalle { get; } = new Lista<string>();
        public double Litros { get; set; }
        public double Lambda { get; set; }
        public bool Factible { get; set; } = true;
        public double TotalNecesidad { get; set; }
        public double TotalMinimo { get; set; }
        public double TotalAsignado { get; set; }
    }

    // Reparte un presupuesto fijo de agua entre las zonas con MULTIPLICADORES DE LAGRANGE.
    //
    // Problema: con B litros (lo que alcanza el dinero) no se puede dar a cada zona todo
    // lo que necesita (Wᵢ). ¿Cuánto (xᵢ) le toca a cada una?
    //
    //   minimizar   D(x) = Σ pᵢ · (Wᵢ − xᵢ)²          "daño" por el agua que falta
    //   sujeto a    Σ xᵢ = B                         se usa todo el presupuesto
    //               xᵢ ≥ Mᵢ                          humedad mínima garantizada
    //
    // pᵢ = Kcᵢ / Aᵢ: el déficit pesa más en plantas que consumen más (Kc alto) y, al dividir
    // por el área, se compara la lámina que falta (mm) y no los litros.
    //
    // Lagrangiano:  L(x, λ) = Σ pᵢ (Wᵢ − xᵢ)² + λ (Σ xᵢ − B)
    //   ∂L/∂xᵢ = −2 pᵢ (Wᵢ − xᵢ) + λ = 0   →   xᵢ = Wᵢ − λ / (2 pᵢ)
    //   ∂L/∂λ  = Σ xᵢ − B = 0             →   λ = 2 (Σ Wᵢ − B) / Σ (1/pᵢ)
    //
    // Si a una zona le toca menos que su mínimo, se fija en Mᵢ (la restricción queda
    // "activa") y se vuelve a calcular λ con las demás. Esto son las condiciones de
    // Karush-Kuhn-Tucker, la extensión de Lagrange para restricciones de desigualdad.
    // λ es el "precio sombra": cuánto bajaría el daño con un litro más de presupuesto.
    internal static class RepartoLagrange
    {
        public static ResultadoReparto Repartir(Lista<ZonaReparto> zonas, double litros)
        {
            ResultadoReparto r = new ResultadoReparto();
            r.Litros = litros;
            foreach (ZonaReparto z in zonas)
            {
                r.Zonas.Add(z);
                r.TotalNecesidad += z.Necesidad;
                r.TotalMinimo += z.Minimo;
            }

            if (r.Zonas.Count == 0)
            {
                r.Detalle.Add("No hay zonas activas para repartir.");
                return r;
            }

            // Caso 1: alcanza para todo. Cada zona recibe lo que necesita y sobra agua.
            if (litros >= r.TotalNecesidad)
            {
                foreach (ZonaReparto z in r.Zonas)
                {
                    z.Asignado = z.Necesidad;
                }
                r.TotalAsignado = r.TotalNecesidad;
                r.Detalle.Add($"El presupuesto alcanza para todo: sobran {litros - r.TotalNecesidad:N0} L. λ = 0 (la restricción no limita).");
                return r;
            }

            // Caso 2: ni siquiera alcanza para los mínimos: el problema no tiene solución.
            if (litros < r.TotalMinimo)
            {
                r.Factible = false;
                foreach (ZonaReparto z in r.Zonas)
                {
                    // Se reparte en proporción al mínimo solo para mostrar algo.
                    z.Asignado = r.TotalMinimo <= 0 ? 0 : litros * z.Minimo / r.TotalMinimo;
                    z.EnMinimo = true;
                }
                r.TotalAsignado = litros;
                r.Detalle.Add($"NO ES POSIBLE: la humedad mínima de todas las zonas necesita {r.TotalMinimo:N0} L "
                    + $"y el presupuesto da {litros:N0} L. Faltan {r.TotalMinimo - litros:N0} L.");
                return r;
            }

            // Caso 3: Lagrange con las zonas "libres"; las que bajan del mínimo se fijan.
            int vueltas = 0;
            bool huboCambios = true;
            while (huboCambios)
            {
                vueltas++;
                double litrosLibres = litros;
                double sumaW = 0;
                double sumaInversos = 0;

                foreach (ZonaReparto z in r.Zonas)
                {
                    if (z.EnMinimo)
                    {
                        litrosLibres -= z.Minimo;
                    }
                    else
                    {
                        sumaW += z.Necesidad;
                        sumaInversos += 1 / z.Peso;
                    }
                }

                r.Lambda = 2 * (sumaW - litrosLibres) / sumaInversos;

                huboCambios = false;
                foreach (ZonaReparto z in r.Zonas)
                {
                    if (z.EnMinimo)
                    {
                        z.Asignado = z.Minimo;
                        continue;
                    }

                    z.Asignado = z.Necesidad - r.Lambda / (2 * z.Peso);
                    if (z.Asignado < z.Minimo)
                    {
                        z.EnMinimo = true;      // restricción activa: se fija y se repite
                        huboCambios = true;
                    }
                }
            }

            foreach (ZonaReparto z in r.Zonas)
            {
                r.TotalAsignado += z.Asignado;
            }

            r.Detalle.Add($"Multiplicador λ = {r.Lambda:0.####} (calculado en {vueltas} vuelta(s))");
            r.Detalle.Add("Comprobación: en cada zona libre 2·pᵢ·(Wᵢ − xᵢ) debe ser igual a λ");
            foreach (ZonaReparto z in r.Zonas)
            {
                double derivada = 2 * z.Peso * (z.Necesidad - z.Asignado);
                string tipo = z.EnMinimo ? "en su mínimo (≤ λ)" : "libre (= λ)";
                r.Detalle.Add($"   {z.Zona.Nombre}: 2·pᵢ·(Wᵢ − xᵢ) = {derivada:0.####}  {tipo}");
            }
            return r;
        }

        // El "daño" D(x) = Σ pᵢ (Wᵢ − xᵢ)²: lo que Lagrange minimiza. Sirve para comprobar.
        public static double Dano(Lista<ZonaReparto> zonas)
        {
            double suma = 0;
            foreach (ZonaReparto z in zonas)
            {
                suma += z.Peso * (z.Necesidad - z.Asignado) * (z.Necesidad - z.Asignado);
            }
            return suma;
        }
    }
}
