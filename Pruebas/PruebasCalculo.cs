using Proyecto.Calculo;
using Proyecto.Domain;
using Proyecto.DynamicStructure;

namespace Proyecto.Pruebas
{
    // Pruebas de los cálculos del plan de riego: la integral de flujo y el reparto de Lagrange.
    // VelocidadMaxima y Dano no los usa el programa; sirven solo para comprobar.
    internal static class PruebasCalculo
    {
        public static void Ejecutar(Action<bool, string> revisar)
        {
            PruebaFlujo(revisar);
            PruebaLagrange(revisar);
        }

        // ---------------- Integral de flujo ----------------
        // La integral doble con Simpson debe dar lo mismo que la fórmula exacta πR²vmax/2,
        // y al despejar vmax del caudal se debe recuperar la velocidad de partida.
        private static void PruebaFlujo(Action<bool, string> revisar)
        {
            double radio = 0.0254;      // los mismos valores del tubo principal del programa
            double vmax = 1.2;

            double simpson = Integrales.FlujoTubo(radio, vmax);
            double exacto = Math.PI * radio * radio * vmax / 2;
            double error = Math.Abs(simpson - exacto) / exacto * 100;
            Console.WriteLine($"   Caudal con Simpson: {simpson * 60000:0.000} L/min   exacto: {exacto * 60000:0.000} L/min   error: {error:0.######} %");
            revisar(error < 0.001, "La integral de flujo coincide con la fórmula exacta πR²vmax/2");

            double recuperada = VelocidadMaxima(simpson, radio);
            revisar(Math.Abs(recuperada - vmax) < 1e-6, $"Al despejar vmax del caudal se recupera {vmax} m/s");
        }

        // Velocidad en el centro del tubo para que pase el caudal Q (m³/s): vmax = 2Q / (πR²)
        private static double VelocidadMaxima(double caudal, double radio)
        {
            return 2 * caudal / (Math.PI * radio * radio);
        }

        // ---------------- Reparto de Lagrange ----------------
        // Lagrange busca el reparto con el menor "daño" D(x) = Σ pᵢ (Wᵢ − xᵢ)².
        // Se comprueba que: usa todo el presupuesto, respeta los mínimos y que cualquier
        // otro reparto válido (moviendo litros entre zonas) tiene un daño mayor.
        private static void PruebaLagrange(Action<bool, string> revisar)
        {
            Lista<ZonaReparto> zonas = new Lista<ZonaReparto>();
            zonas.Add(Zona(1, "Prado", 40, 50, 0.80, necesidad: 18000, minimo: 9000));
            zonas.Add(Zona(2, "Cancha", 42, 50, 0.85, necesidad: 20000, minimo: 10000));
            zonas.Add(Zona(3, "Huerta", 8, 12, 1.05, necesidad: 3000, minimo: 2500));
            zonas.Add(Zona(4, "Jardín", 15, 20, 0.70, necesidad: 6000, minimo: 3000));
            double presupuesto = 28000;

            ResultadoReparto r = RepartoLagrange.Repartir(zonas, presupuesto);
            ZonaReparto[] z = r.Zonas.ToArray();

            revisar(r.Factible && Math.Abs(r.TotalAsignado - presupuesto) < 1e-6,
                $"Lagrange reparte exactamente los {presupuesto:N0} L del presupuesto");
            revisar(z.All(x => x.Asignado >= x.Minimo - 1e-6), "Ninguna zona queda por debajo de su mínimo");

            double danoLagrange = Dano(z);
            double danoMinimo = danoLagrange;
            int peores = 0;
            int intentos = 0;
            for (int i = 0; i < z.Length; i++)
            {
                for (int j = 0; j < z.Length; j++)
                {
                    foreach (double litros in new[] { 1.0, 50, 500 })
                    {
                        // Pasar litros de la zona i a la j sin romper el mínimo de i.
                        if (i == j || z[i].Asignado - litros < z[i].Minimo)
                        {
                            continue;
                        }
                        double[] otro = z.Select(x => x.Asignado).ToArray();
                        otro[i] -= litros;
                        otro[j] += litros;
                        double dano = Dano(z, otro);
                        intentos++;
                        if (dano > danoLagrange)
                        {
                            peores++;
                        }
                        danoMinimo = Math.Min(danoMinimo, dano);
                    }
                }
            }

            Console.WriteLine($"   Daño con Lagrange: {danoLagrange:N1}   λ = {r.Lambda:0.####}   "
                + $"repartos alternativos probados: {intentos}, todos con más daño: {(peores == intentos ? "sí" : "no")}");
            revisar(peores == intentos, "Cualquier otro reparto válido tiene más daño que el de Lagrange");
        }

        // D(x) = Σ pᵢ (Wᵢ − xᵢ)², con lo asignado por Lagrange o con otro reparto.
        private static double Dano(ZonaReparto[] zonas, double[]? asignado = null)
        {
            double suma = 0;
            for (int i = 0; i < zonas.Length; i++)
            {
                double x = asignado == null ? zonas[i].Asignado : asignado[i];
                double falta = zonas[i].Necesidad - x;
                suma += zonas[i].Peso * falta * falta;
            }
            return suma;
        }

        private static ZonaReparto Zona(long id, string nombre, double ancho, double largo, double kc, double necesidad, double minimo)
        {
            ZonaVerde zona = ZonaVerde.Create(id, nombre, TipoVegetacion.Cesped, ancho, largo, 0, 0, kc, 30, 0.3);
            return new ZonaReparto(zona, 35, necesidad, minimo);
        }
    }
}
