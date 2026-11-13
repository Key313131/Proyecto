using Proyecto.Contracts;
using Proyecto.Exceptions;
using Proyecto.Tools;

namespace Proyecto.Domain
{
    internal class Lectura : EntidadBase, IFactory<Lectura>, ISerialize<Lectura>
    {
        private long _sensorId = 1;
        private double _valor;

        public long SensorId
        {
            get
            { 
                return _sensorId; 
            }
            set
            {
                if (value <= 0)
                {
                    throw new DomainExceptions("La lectura debe pertenecer a un sensor válido.");
                }   
                _sensorId = value;
            }
        }

        public DateTime FechaHora { get; set; } = DateTime.Now;
        public double Valor
        {
            get
            {
                return _valor;
            }
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value))
                {
                    throw new DomainExceptions("El valor de la lectura no es un número válido.");
                }
                _valor = value;
            }
        }

        private Lectura() { }

        public static Lectura Create()
        {
            return new Lectura();
        }   
        public static Lectura Create(string textCsv) 
        {
            return Deserialize(textCsv);
        }

        public static Lectura Create(long id, long sensorId, DateTime fechaHora, double valor)
        {
            return new Lectura { Id = id, SensorId = sensorId, FechaHora = fechaHora, Valor = valor };
        }

        // Formato: Id;SensorId;FechaHora;Valor
        private const int Campos = 4;

        public static string Serialize(Lectura l)
        {
            return Csv.Unir(l.Id, l.SensorId, Csv.Fecha(l.FechaHora), l.Valor);
        }

        public static Lectura Deserialize(string textCsv)
        {
            string[] campos = Csv.Partir(textCsv, Campos);
            try
            {
                return Create(Csv.Long(campos[0]), Csv.Long(campos[1]), Csv.Fecha(campos[2]), Csv.Double(campos[3]));
            }
            catch (Exception ex) when (ex is FormatException or OverflowException
                                          or ArgumentException or DomainExceptions)
            {
                throw new DatosCorruptosException("No se pudo leer la lectura", textCsv, ex);
            }
        }

        public override string ToString() 
        { 
            return $"{FechaHora:yyyy-MM-dd HH:mm} | Sensor {SensorId} | {Valor:N2}"; 
        }
    }


}
