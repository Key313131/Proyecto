using Proyecto.Contracts;
using Proyecto.Exceptions;
using Proyecto.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Domain
{
    internal class ZonaVerde : EntidadBase, IFactory<ZonaVerde>, ISerialize<ZonaVerde>
    {
        private string _nombre = "Sin nombre";
        private double _areaM2 = 1;
        private double _kc = 1.0; // coeficiente de cultivo 
        private double _humedadMinima = 30;
        private double _profundidadRaizM = 0.3;

        public string Nombre
        {
            get 
            { 
                return _nombre;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new DomainExceptions("El nombre de la zona es obligatorio.");
                }

                if (value.Contains(Csv.Separador))
                {
                    throw new DomainExceptions($"El nombre no puede contener '{Csv.Separador}'.");
                }
                    
                _nombre = value.Trim();
            }
        }

        public TipoVegetacion Vegetacion { get; set; } = TipoVegetacion.Cesped;

        public double AreaM2
        {
            get 
            { 
                return _areaM2; 
            }
            set
            {
                if (value <= 0)
                {
                    throw new DomainExceptions("El área debe ser mayor que 0 m².");
                }
                _areaM2 = value;
            }
        }

        // coordenadas en metros 
        public double X { get; set; }
        public double Y { get; set; }

        // Coeficiente de cultivo (Kc, metodología FAO-56). Indica cuánta agua necesita
        // la planta comparada con un césped de referencia. Valores típicos: 0.3 a 1.2.

        public double Kc
        {
            get 
            { 
                return _kc; 
            }
            set
            {
                if (value < 0.1 || value > 1.5)
                {
                    throw new DomainExceptions("El coeficiente Kc debe estar entre 0.1 y 1.5.");
                }
                _kc = value;
            }
        }

        // % de humedad del suelo por debajo del cual se debe regar
        public double HumedadMinima
        {
            get 
            { 
                return _humedadMinima; 
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new DomainExceptions("La humedad mínima debe estar entre 0 y 100 %.");
                }
                    
                _humedadMinima = value;
            }
        }

        // en metros
        public double ProfundidadRaizM
        {
            get 
            { 
                return _profundidadRaizM; 
            }
            set
            {
                if (value <= 0 || value > 3)
                {
                    throw new DomainExceptions("La profundidad de raíz debe estar entre 0 y 3 m.");
                }
                _profundidadRaizM = value;
            }
        }

        private ZonaVerde() { }

        public static ZonaVerde Create() 
        {
            return new ZonaVerde();
        }

        public static ZonaVerde Create(string textCsv) 
        { 
            return Deserialize(textCsv); 
        }

        public static ZonaVerde Create(long id, string nombre, TipoVegetacion vegetacion, double areaM2,
                                      double x, double y, double kc, double humedadMinima, double profundidadRaizM)
        {
            return new ZonaVerde
            {
                Id = id,
                Nombre = nombre,
                Vegetacion = vegetacion,
                AreaM2 = areaM2,
                X = x,
                Y = y,
                Kc = kc,
                HumedadMinima = humedadMinima,
                ProfundidadRaizM = profundidadRaizM
            };
        }

        // Formato: Id;Nombre;Vegetacion;AreaM2;X;Y;Kc;HumedadMinima;ProfundidadRaizM
        private const int Campos = 9;

        public static string Serialize(ZonaVerde z)
        {
            return Csv.Unir(z.Id, z.Nombre, z.Vegetacion, z.AreaM2, z.X, z.Y,
                            z.Kc, z.HumedadMinima, z.ProfundidadRaizM);
        }

        public static ZonaVerde Deserialize(string textCsv)
        {
            string[] campos = Csv.Partir(textCsv, Campos);
            try
            {
                return Create( Csv.Long(campos[0]), 
                                 campos[1], 
                             Csv.Enum<TipoVegetacion>(campos[2]),
                             Csv.Double(campos[3]), 
                             Csv.Double(campos[4]), 
                             Csv.Double(campos[5]),
                             Csv.Double(campos[6]), 
                             Csv.Double(campos[7]), 
                             Csv.Double(campos[8]) );
            }
            catch (Exception ex) when (ex is FormatException or OverflowException or ArgumentException or DomainExceptions)
            {
                throw new DatosCorruptosException("No se pudo leer la zona verde", textCsv, ex);
            }
        }

        public override string ToString()
        {
            return $"[{Id}] {Nombre} ({Vegetacion}) - {AreaM2: N1} m²";
        }
    }
}
