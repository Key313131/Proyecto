namespace Proyecto.Exceptions
{
    internal class DatosCorruptosException : Exception
    {
        public string Linea { get; }

        public DatosCorruptosException( // ====
            string mensaje,
            string linea,
            Exception? causa = null)
            : base($"{mensaje} | Línea: \"{linea}\"", causa) // ====
        {
            Linea = linea;
        }
    }
}
