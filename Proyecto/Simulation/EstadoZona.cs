namespace Proyecto.Simulation
{
    internal class EstadoZona
    {
        public long ZonaId { get; }
        public double Humedad { get; set; }    
        public bool Regando { get; set; }

        public EstadoZona(long zonaId, double humedad)
        {
            ZonaId = zonaId;
            Humedad = humedad;
        }
    }
}
