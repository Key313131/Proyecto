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
        private double _anchoM = 1;
        private double _largoM = 1;
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

        public double AnchoM
        {
            get
            {
                return _anchoM;
            }
            set
            {
                if (value <= 0)
                {
                    throw new DomainExceptions("El ancho debe ser mayor que 0 m.");
                }
                _anchoM = value;
            }
        }

        public double LargoM
        {
            get
            {
                return _largoM;
            }
            set
            {
                if (value <= 0)
                {
                    throw new DomainExceptions("El largo debe ser mayor que 0 m.");
                }
                _largoM = value;
            }
        }

        // El área se calcula: así nunca puede contradecir al ancho y al largo.
        public double AreaM2 => AnchoM * LargoM;

        // Una zona no se borra, se desactiva: su Id nunca se vuelve a usar.
        public bool Activo { get; set; } = true;

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

        public static ZonaVerde Create(long id, string nombre, TipoVegetacion vegetacion, double anchoM, double largoM,
                                      double x, double y, double kc, double humedadMinima, double profundidadRaizM,
                                      bool activo = true)
        {
            return new ZonaVerde
            {
                Id = id,
                Nombre = nombre,
                Vegetacion = vegetacion,
                AnchoM = anchoM,
                LargoM = largoM,
                X = x,
                Y = y,
                Kc = kc,
                HumedadMinima = humedadMinima,
                ProfundidadRaizM = profundidadRaizM,
                Activo = activo
            };
        }

        // Devuelve una copia: si el repositorio rechaza el cambio, la original queda intacta.
        public ZonaVerde ConEstado(bool activo)
        {
            return Create(Id, Nombre, Vegetacion, AnchoM, LargoM, X, Y, Kc,
                          HumedadMinima, ProfundidadRaizM, activo);
        }

        // Formato: Id;Nombre;Vegetacion;AnchoM;LargoM;X;Y;Kc;HumedadMinima;ProfundidadRaizM;Activo
        private const int Campos = 11;

        public static string Serialize(ZonaVerde z)
        {
            return Csv.Unir(z.Id, z.Nombre, z.Vegetacion, z.AnchoM, z.LargoM, z.X, z.Y,
                            z.Kc, z.HumedadMinima, z.ProfundidadRaizM, z.Activo);
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
                             Csv.Double(campos[8]),
                             Csv.Double(campos[9]),
                             Csv.Bool(campos[10]) );
            }
            catch (Exception ex) when (ex is FormatException or OverflowException or ArgumentException or DomainExceptions)
            {
                throw new DatosCorruptosException("No se pudo leer la zona verde", textCsv, ex);
            }
        }

        public override string ToString()
        {
            return $"[{Id}] {Nombre} ({Vegetacion}) - {AreaM2:N1} m²{(Activo ? "" : " [inactiva]")}";
        }
    }
}
