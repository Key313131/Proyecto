using Proyecto.Algorithms;
using Proyecto.Domain;
using Proyecto.DynamicStructure;

namespace Proyecto.Calculo
{
    // Lo que el usuario decide para el día.
    internal class ParametrosRiego
    {
        public double Temperatura { get; init; } = 22;          // °C previstos
        public double HorasSol { get; init; } = 12;             // horas de luz del día
        public double Presupuesto { get; init; }                // $ disponibles hoy
        public double Tarifa { get; init; } = 5000;             // $ por m³
        public TimeSpan Inicio { get; init; } = TimeSpan.FromHours(5);
        public double HorasDisponibles { get; init; } = 3;      // cuánto tiempo puede regarse
    }

    // Lo que se midió en cada zona (lo junta el servicio a partir de las lecturas).
    internal class EntradaZona
    {
        public ZonaVerde Zona { get; init; } = null!;
        public double Humedad { get; init; }
        public bool Lloviendo { get; init; }
        public bool SinLecturas { get; init; }
    }

    internal class FilaPlan
    {
        public ZonaVerde Zona { get; init; } = null!;
        public double Humedad { get; init; }
        public bool Regar { get; set; }
        public string Motivo { get; set; } = "";
        public double Necesidad { get; init; }      // W: lo ideal
        public double Minimo { get; init; }         // lo necesario para la humedad mínima
        public double Litros { get; set; }          // lo que se le asigna
        public bool EnMinimo { get; set; }
        public TimeSpan? Desde { get; set; }
        public TimeSpan? Hasta { get; set; }
        public double Costo { get; set; }
        public double PorcentajeCubierto => Necesidad <= 0 ? 100 : Litros / Necesidad * 100;
    }

    internal class PlanRiego
    {
        public Lista<FilaPlan> Zonas { get; } = new Lista<FilaPlan>();
        public double CaudalPrincipal { get; set; }     // L/min por el tubo principal
        public double AguaDisponible { get; set; }      // L en la ventana de riego
        public double LitrosPorDinero { get; set; }     // L que se pueden pagar
        public double Limite { get; set; }             // B = min(agua, dinero)
        public bool LimitaElDinero { get; set; }
        public bool Factible { get; set; } = true;
        public double TotalNecesidad { get; set; }
        public double TotalMinimo { get; set; }
        public double TotalLitros { get; set; }
        public double CostoTotal { get; set; }
        public double Lambda { get; set; }
        public Lista<string> Resumen { get; } = new Lista<string>();   // para el usuario
        public Lista<string> Detalle { get; } = new Lista<string>();   // "Ver cálculo detallado"
    }

    // Arma el plan de riego del día. Es matemática pura: no lee archivos, así se puede probar.
    //
    //  1. ¿Cuánta agua necesita cada zona?   W(T, H, A, S)          (función de varias variables)
    //  2. ¿Cuánta agua tenemos?              Q = ∬ v·n dS (tubo), V = Q · t   (integral doble de flujo)
    //  3. ¿Cuánta podemos pagar?             presupuesto / tarifa
    //  4. ¿Cómo la repartimos si no alcanza? Lagrange con humedad mínima garantizada
    //  5. ¿A qué hora riega cada zona?       una zona tras otra, la más seca primero
    internal static class PlanificadorRiego
    {
        // Tubo principal de 2 pulgadas (5,08 cm de diámetro) y velocidad del agua en su centro.
        public const double RadioTuboPrincipal = 0.0254;    // m
        public const double VelocidadCentro = 1.2;          // m/s

        // Caudal del tubo principal en L/min, calculado con la integral de flujo.
        public static double CaudalPrincipal()
        {
            double metrosCubicosPorSegundo = Integrales.FlujoTubo(RadioTuboPrincipal, VelocidadCentro);
            return metrosCubicosPorSegundo * 1000 * 60;
        }

        public static PlanRiego Calcular(IEnumerable<EntradaZona> entradas, ParametrosRiego p)
        {
            PlanRiego plan = new PlanRiego();
            Lista<ZonaReparto> aRegar = new Lista<ZonaReparto>();

            // 1. Necesidad de cada zona y decisión de regar o no.
            foreach (EntradaZona e in entradas)
            {
                VariablesAgua v = new VariablesAgua(p.Temperatura, e.Humedad, e.Zona.AreaM2, p.HorasSol);
                double objetivo = ModeloAgua.HumedadObjetivo(e.Zona);
                double necesidad = ModeloAgua.Necesidad(e.Zona, v);
                double minimo = ModeloAgua.Necesidad(e.Zona, v, e.Zona.HumedadMinima);

                FilaPlan fila = new FilaPlan { Zona = e.Zona, Humedad = e.Humedad, Necesidad = necesidad, Minimo = minimo };
                if (e.Lloviendo)
                {
                    fila.Motivo = "No: está lloviendo";
                }
                else if (e.Humedad >= objetivo)
                {
                    fila.Motivo = $"No: humedad suficiente ({e.Humedad:0.#} %)";
                }
                else
                {
                    fila.Regar = true;
                    fila.Motivo = e.Humedad < e.Zona.HumedadMinima
                        ? $"Sí: muy seco ({e.Humedad:0.#} % < {e.Zona.HumedadMinima:0.#} %)"
                        : $"Sí: bajo el objetivo ({e.Humedad:0.#} % < {objetivo:0.#} %)";
                    if (e.SinLecturas)
                    {
                        fila.Motivo += ", sin lecturas";
                    }
                    aRegar.Add(new ZonaReparto(e.Zona, e.Humedad, necesidad, minimo));
                    plan.TotalNecesidad += necesidad;
                    plan.TotalMinimo += minimo;
                }
                plan.Zonas.Add(fila);
            }

            // 2. Agua disponible: caudal del tubo principal × tiempo de la ventana de riego.
            plan.CaudalPrincipal = CaudalPrincipal();
            plan.AguaDisponible = plan.CaudalPrincipal * p.HorasDisponibles * 60;

            // 3. Agua que se puede pagar.
            plan.LitrosPorDinero = p.Tarifa <= 0 ? 0 : p.Presupuesto / p.Tarifa * 1000;

            plan.LimitaElDinero = plan.LitrosPorDinero < plan.AguaDisponible;
            plan.Limite = Math.Min(plan.AguaDisponible, plan.LitrosPorDinero);

            // 4. Reparto con Lagrange (si alcanza para todo, cada zona recibe lo que necesita).
            ResultadoReparto reparto = RepartoLagrange.Repartir(aRegar, plan.Limite);
            plan.Factible = reparto.Factible;
            plan.Lambda = reparto.Lambda;
            foreach (ZonaReparto z in reparto.Zonas)
            {
                FilaPlan fila = plan.Zonas.FirstOrDefault(f => f.Zona.Id == z.Zona.Id)!;
                fila.Litros = z.Asignado;
                fila.EnMinimo = z.EnMinimo && reparto.Factible;
                fila.Costo = z.Asignado / 1000 * p.Tarifa;
                plan.TotalLitros += z.Asignado;
                plan.CostoTotal += fila.Costo;
            }

            // 5. Horario: se abre la válvula de una zona a la vez, empezando por la más seca
            //    respecto a su mínimo. Duración = litros / caudal.
            FilaPlan[] orden = plan.Zonas.Where(f => f.Regar && f.Litros > 0).ToArray();
            Ordenamiento.MergeSort(orden, (a, b) =>
                (a.Humedad - a.Zona.HumedadMinima).CompareTo(b.Humedad - b.Zona.HumedadMinima));
            TimeSpan reloj = p.Inicio;
            foreach (FilaPlan fila in orden)
            {
                TimeSpan duracion = TimeSpan.FromMinutes(fila.Litros / plan.CaudalPrincipal);
                fila.Desde = reloj;
                fila.Hasta = reloj + duracion;
                reloj += duracion;
            }

            EscribirResumen(plan, p, orden);
            EscribirDetalle(plan, p, reparto);
            return plan;
        }

        // Lo esencial, en palabras del usuario.
        private static void EscribirResumen(PlanRiego plan, ParametrosRiego p, FilaPlan[] orden)
        {
            int total = plan.Zonas.Count;
            int necesitan = plan.Zonas.Count(f => f.Regar);
            if (necesitan == 0)
            {
                plan.Resumen.Add(total == 0 ? "No hay zonas activas." : "Hoy no hace falta regar ninguna zona.");
                return;
            }

            if (orden.Length > 0)
            {
                TimeSpan fin = orden[^1].Hasta!.Value;
                plan.Resumen.Add($"Hoy se riegan {orden.Length} de {total} zonas: {plan.TotalLitros:N0} L por {plan.CostoTotal:N0} $, "
                    + $"de {Hora(p.Inicio)} a {Hora(fin)}.");
            }
            else
            {
                // Hay zonas secas pero el límite es 0 (sin presupuesto o sin horas de riego).
                plan.Resumen.Add($"{necesitan} zona(s) necesitan agua, pero hoy no hay agua ni presupuesto para regar.");
            }

            if (!plan.Factible)
            {
                double falta = plan.TotalMinimo - plan.Limite;
                plan.Resumen.Add($"ATENCIÓN: no alcanza para mantener la humedad mínima. Faltan {falta:N0} L "
                    + (plan.LimitaElDinero ? $"({falta / 1000 * p.Tarifa:N0} $ más de presupuesto)." : "(más horas de riego)."));
            }
            else if (plan.TotalLitros < plan.TotalNecesidad - 0.5)
            {
                string causa = plan.LimitaElDinero ? "el presupuesto" : $"el agua disponible en {p.HorasDisponibles:0.#} h";
                plan.Resumen.Add($"No alcanza para todo: se cubre el {plan.TotalLitros / plan.TotalNecesidad * 100:0} % "
                    + $"(lo limita {causa}). Se repartió protegiendo la humedad mínima de cada zona.");
            }
            else
            {
                double sobra = plan.Limite - plan.TotalLitros;
                plan.Resumen.Add(plan.LimitaElDinero
                    ? $"Alcanza para todo. Sobran {sobra / 1000 * p.Tarifa:N0} $ del presupuesto."
                    : $"Alcanza para todo. Quedan {sobra / plan.CaudalPrincipal:N0} min libres en la ventana de riego.");
            }
        }

        // El "por qué" de cada número, para quien quiera verlo (y para la sustentación).
        private static void EscribirDetalle(PlanRiego plan, ParametrosRiego p, ResultadoReparto reparto)
        {
            Lista<string> d = plan.Detalle;
            double et0 = ModeloAgua.Et0(p.Temperatura, p.HorasSol);

            d.Add("1. NECESIDAD DE AGUA DE CADA ZONA (función de varias variables)");
            d.Add("   W(T, H, A, S) = A · [ Kc · ET₀(T, S) + max(0, Hobj − H) / 100 · Pr · 1000 · θ ]");
            d.Add($"   ET₀ (Blaney-Criddle) = p · (0,46·T + 8,13), p = S/4380·100 → con T = {p.Temperatura:0.#} °C y S = {p.HorasSol:0.#} h: "
                + $"ET₀ = {et0:0.##} mm/día. θ = {ModeloAgua.Porosidad}");
            foreach (FilaPlan f in plan.Zonas)
            {
                d.Add($"   {f.Zona.Nombre}: A = {f.Zona.AreaM2:N1} m², Kc = {f.Zona.Kc}, Pr = {f.Zona.ProfundidadRaizM} m, "
                    + $"H = {f.Humedad:0.#} %, Hobj = {ModeloAgua.HumedadObjetivo(f.Zona):0.#} % → W = {f.Necesidad:N1} L, "
                    + $"mínimo M = {f.Minimo:N1} L. {f.Motivo}");
            }

            double q = plan.CaudalPrincipal / 1000 / 60;
            double formula = Math.PI * RadioTuboPrincipal * RadioTuboPrincipal * VelocidadCentro / 2;
            d.Add("2. AGUA DISPONIBLE (integral doble de flujo)");
            d.Add($"   Tubo principal: R = {RadioTuboPrincipal * 100:0.##} cm, velocidad en el centro {VelocidadCentro} m/s, perfil v(r) = vmax(1 − r²/R²)");
            d.Add($"   Q = ∬ v·n dS = ∫₀^2π ∫₀^R vmax(1 − r²/R²)·r dr dθ = {q:0.######} m³/s (fórmula πR²vmax/2 = {formula:0.######}) "
                + $"= {plan.CaudalPrincipal:0.##} L/min");
            d.Add($"   V = Q · t = {plan.CaudalPrincipal:0.##} L/min × {p.HorasDisponibles * 60:0} min = {plan.AguaDisponible:N0} L");

            d.Add("3. AGUA QUE SE PUEDE PAGAR");
            d.Add($"   {p.Presupuesto:N0} $ / {p.Tarifa:N0} $/m³ = {plan.LitrosPorDinero:N0} L");
            d.Add($"   Límite del día B = min({plan.AguaDisponible:N0}, {plan.LitrosPorDinero:N0}) = {plan.Limite:N0} L "
                + $"(lo limita {(plan.LimitaElDinero ? "el dinero" : "el agua")})");

            d.Add("4. REPARTO CON MULTIPLICADORES DE LAGRANGE");
            d.Add("   minimizar D = Σ pᵢ (Wᵢ − xᵢ)²  sujeto a  Σ xᵢ = B  y  xᵢ ≥ Mᵢ,  con pᵢ = Kcᵢ / Aᵢ");
            d.Add("   ∂L/∂xᵢ = −2pᵢ(Wᵢ − xᵢ) + λ = 0  →  xᵢ = Wᵢ − λ/(2pᵢ),   λ = 2(ΣWᵢ − B) / Σ(1/pᵢ)");
            d.Add($"   Necesidad total ΣW = {reparto.TotalNecesidad:N0} L, mínimo total ΣM = {reparto.TotalMinimo:N0} L");
            foreach (string linea in reparto.Detalle)
            {
                d.Add("   " + linea);
            }

            d.Add("5. HORARIO (una válvula a la vez, la zona más seca primero)");
            d.Add($"   Duración de cada zona = litros / Q = litros / {plan.CaudalPrincipal:0.##} L/min");
            foreach (FilaPlan f in plan.Zonas)
            {
                if (f.Desde != null)
                {
                    d.Add($"   {f.Zona.Nombre}: {f.Litros:N0} L / {plan.CaudalPrincipal:0.##} = {(f.Hasta - f.Desde)!.Value.TotalMinutes:0.#} min "
                        + $"→ {Hora(f.Desde.Value)} a {Hora(f.Hasta!.Value)}");
                }
            }
        }

        public static string Hora(TimeSpan t)
        {
            // Si el riego pasa de medianoche se muestra la hora del día siguiente.
            TimeSpan delDia = TimeSpan.FromMinutes(Math.Round(t.TotalMinutes) % (24 * 60));
            return $"{(int)delDia.TotalHours:00}:{delDia.Minutes:00}";
        }
    }
}
