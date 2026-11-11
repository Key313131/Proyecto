using Proyecto.Domain;
using Proyecto.DynamicStructure;
using Proyecto.Simulation;

namespace Proyecto.Calculo
{
    // Todos los cálculos de una zona juntos, con los números (para las pruebas)
    // y el texto que se muestra en la pantalla.
    internal class ResultadoZona
    {
        public double Necesidad { get; set; }               // W, litros/día
        public double Lamina { get; set; }                  // mm/día
        public double[] GradienteExacto { get; set; } = Array.Empty<double>();
        public double[] GradienteNumerico { get; set; } = Array.Empty<double>();
        public double HumedadPromedio { get; set; }         // (1/A) ∬ h dA
        public (double dx, double dy) GradienteHumedad { get; set; }
        public double PendienteTerreno { get; set; }        // %
        public double AreaRectangulo { get; set; }
        public double AreaIntegral { get; set; }
        public double AreaGreen { get; set; }
        public double VolumenFaltanteCampo { get; set; }    // L, con el campo h(x, y)
        public double VolumenFaltantePromedio { get; set; } // L, suponiendo la humedad promedio en todo
        public double VolumenSuelo { get; set; }            // m³ = ∭ dV
        public double AguaEnSuelo { get; set; }             // L = ∭ θ·h/100 dV
        public double Caudal { get; set; }                  // L/min
        public double VelocidadMaxima { get; set; }         // m/s
        public double CaudalIntegral { get; set; }          // L/min, con la integral de flujo
        public Lista<string> Detalle { get; } = new Lista<string>();
    }

    internal static class AnalisisZona
    {
        // Tubo de 1 pulgada (2,54 cm de diámetro) que lleva el agua a cada zona.
        public const double RadioTubo = 0.0127;

        // "campo" puede ser null o vacío si la zona no tiene sensores de humedad con lecturas:
        // entonces se supone que toda la zona tiene la humedad de "v".
        public static ResultadoZona Calcular(ZonaVerde zona, VariablesAgua v, CampoHumedad? campo)
        {
            ResultadoZona r = new ResultadoZona();
            double ancho = zona.AnchoM;
            double largo = zona.LargoM;
            double hObjetivo = ModeloAgua.HumedadObjetivo(zona);
            bool hayCampo = campo != null && campo.Sensores > 0;
            Func<double, double, double> humedad = hayCampo ? campo!.Valor : (x, y) => v.Humedad;

            // 1. Función de varias variables W(T, H, A, S)
            r.Necesidad = ModeloAgua.Necesidad(zona, v);
            r.Lamina = r.Necesidad / v.Area;
            double et0 = ModeloAgua.Et0(v.Temperatura, v.HorasSol);
            r.Detalle.Add("1. NECESIDAD DE AGUA  W(T, H, A, S) = A·[Kc·ET₀(T,S) + max(0, Hobj − H)/100·Pr·1000·θ]");
            r.Detalle.Add($"   T = {v.Temperatura:0.#} °C, H = {v.Humedad:0.#} %, A = {v.Area:N1} m², S = {v.HorasSol:0.#} h, "
                + $"Kc = {zona.Kc}, Pr = {zona.ProfundidadRaizM} m, Hobj = {hObjetivo:0.#} %");
            r.Detalle.Add($"   ET₀ = {et0:0.##} mm/día  →  lámina = {r.Lamina:0.##} mm/día  →  W = {r.Necesidad:N1} L/día");

            // 2. Derivadas parciales y gradiente de W: exacto y numérico
            double[] punto = { v.Temperatura, v.Humedad, v.Area, v.HorasSol };
            r.GradienteExacto = ModeloAgua.Gradiente(zona, v);
            r.GradienteNumerico = Derivadas.Gradiente(ModeloAgua.ComoFuncion(zona), punto);
            string[] nombres = { "∂W/∂T", "∂W/∂H", "∂W/∂A", "∂W/∂S" };
            string[] unidades = { "L por °C", "L por % de humedad", "L por m²", "L por hora de luz" };
            r.Detalle.Add("2. DERIVADAS PARCIALES (cuánto cambia W si sube una variable y las demás quedan igual)");
            for (int i = 0; i < 4; i++)
            {
                r.Detalle.Add($"   {nombres[i]} = {r.GradienteExacto[i]:0.###} {unidades[i]}   "
                    + $"(numérica: {r.GradienteNumerico[i]:0.###})");
            }

            // 3. Campo de humedad h(x, y), su promedio (integral doble) y su gradiente
            double area = Integrales.Doble((x, y) => 1, 0, ancho, 0, largo);
            r.HumedadPromedio = hayCampo ? campo!.Promedio() : v.Humedad;
            r.GradienteHumedad = hayCampo ? campo!.Gradiente(ancho / 2, largo / 2) : (0, 0);
            (double hx, double hy) = r.GradienteHumedad;
            double normaH = Derivadas.Norma(hx, hy);
            r.Detalle.Add(hayCampo
                ? $"3. CAMPO DE HUMEDAD h(x, y) con {campo!.Sensores} sensor(es): promedio (1/A)∬h dA = {r.HumedadPromedio:0.##} %"
                : "3. CAMPO DE HUMEDAD: la zona no tiene lecturas de sensores de humedad; se usa H en toda la zona");
            r.Detalle.Add($"   ∇h en el centro = ({hx:0.###}, {hy:0.###}) %/m, |∇h| = {normaH:0.###} %/m. "
                + $"Se seca hacia el {Derivadas.Direccion(-hx, -hy)} (dirección de −∇h)");

            // 4. Terreno z = f(x, y)
            double cx = zona.X + ancho / 2;
            double cy = zona.Y + largo / 2;
            (double zx, double zy) = Terreno.Gradiente(cx, cy);
            r.PendienteTerreno = Terreno.PendientePorcentaje(cx, cy);
            r.Detalle.Add($"4. TERRENO z = f(x, y): altura en el centro {Terreno.Altura(cx, cy):0.##} m, "
                + $"∇z = ({zx:0.####}, {zy:0.####}), pendiente {r.PendienteTerreno:0.##} %, "
                + $"sube hacia el {Derivadas.Direccion(zx, zy)}");

            // 5. Integrales dobles: área y volumen de agua que falta
            r.AreaRectangulo = zona.AreaM2;
            r.AreaIntegral = area;
            r.AreaGreen = Integrales.AreaGreen(new[] { (0.0, 0.0), (ancho, 0.0), (ancho, largo), (0.0, largo) });
            r.Detalle.Add($"5. ÁREA: ancho × largo = {r.AreaRectangulo:N2} m², ∬ 1 dA = {r.AreaIntegral:N2} m², "
                + $"Green ½∮(x dy − y dx) = {r.AreaGreen:N2} m²");

            double factor = zona.ProfundidadRaizM * 1000 * ModeloAgua.Porosidad / 100;   // L/m² por cada % que falta
            r.VolumenFaltanteCampo = Integrales.Doble(
                (x, y) => Math.Max(0, hObjetivo - humedad(x, y)) * factor, 0, ancho, 0, largo, 60);
            r.VolumenFaltantePromedio = Math.Max(0, hObjetivo - r.HumedadPromedio) * factor * area;
            r.Detalle.Add($"   Agua para llevar el suelo a {hObjetivo:0.#} %: ∬ max(0, Hobj − h(x,y))·Pr·θ dA = {r.VolumenFaltanteCampo:N1} L "
                + $"(con la humedad promedio en toda la zona serían {r.VolumenFaltantePromedio:N1} L)");

            // 6. Integrales triples: volumen de suelo y agua guardada en él
            r.VolumenSuelo = Integrales.Triple((x, y, z) => 1, 0, ancho, 0, largo, 0, zona.ProfundidadRaizM, 10);
            r.AguaEnSuelo = Integrales.Triple(
                (x, y, z) => ModeloAgua.Porosidad * humedad(x, y) / 100 * 1000,
                0, ancho, 0, largo, 0, zona.ProfundidadRaizM, 20);
            r.Detalle.Add($"6. VOLUMEN DE SUELO DE LA RAÍZ: ∭ dV = {r.VolumenSuelo:N2} m³ (área × profundidad = {area * zona.ProfundidadRaizM:N2} m³)");
            r.Detalle.Add($"   Agua guardada en ese suelo: ∭ θ·h(x,y)/100 dV = {r.AguaEnSuelo:N0} L");

            // 7. Flujo por el tubo de riego (integral de superficie)
            r.Caudal = SimuladorSensores.CaudalRiego(zona);
            double q = r.Caudal / 1000 / 60;                     // L/min → m³/s
            r.VelocidadMaxima = Integrales.VelocidadMaxima(q, RadioTubo);
            r.CaudalIntegral = Integrales.FlujoTubo(RadioTubo, r.VelocidadMaxima) * 1000 * 60;
            double minutos = r.Necesidad / r.Caudal;
            r.Detalle.Add($"7. FLUJO EN EL TUBO DE 1\": caudal {r.Caudal:0.##} L/min, velocidad en el centro {r.VelocidadMaxima:0.###} m/s");
            r.Detalle.Add($"   ∬ v·n dS = ∫∫ vmax(1 − r²/R²)·r dr dθ = {r.CaudalIntegral:0.##} L/min. "
                + $"Para dar W hay que regar {minutos:N0} min");
            return r;
        }
    }
}
