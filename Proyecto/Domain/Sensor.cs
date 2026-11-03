using Proyecto.Contracts;
using Proyecto.Exceptions;
using Proyecto.Tools;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Proyecto.Domain
{
    internal class Sensor : EntidadBase, IFactory<Sensor>, ISerialize<Sensor>
    {
        private long _zonaId = 1;

        public long ZonaId 
        {   
            get => _zonaId;
            set
            {
                if (value <= 0) 
                { 
                    throw new DomainExceptions("El sensor debe pertenecer a una zona válida.");  
                }
                _zonaId = value;
            } 
        }

        public TipoSensor Tipo { get; set; } = TipoSensor.HumedadSuelo;
        public double x { get; set; }
        public double y { get; set; }
        public bool Activo { get; set; } = true;

        public string Unidad => Tipo switch
        {
            TipoSensor.HumedadSuelo => "%",
            TipoSensor.Caudal => "L/min",
            TipoSensor.Temperatura => "°C",
            TipoSensor.Lluvia => "mm",
            _ => ""
        };
        //public string Unidad
        //{
        //    get
        //    {
        //        switch (Tipo)
        //        {
        //            case TipoSensor.HumedadSuelo:
        //                return "%";
        //            case TipoSensor.Caudal:
        //                return "L/min";
        //            case TipoSensor.Temperatura:
        //                return "°C";
        //            case TipoSensor.Lluvia:
        //                return "mm";
        //            default:
        //                return "";
        //        }
        //    }
        //}
        
        private Sensor() { }
        public static Sensor Create()
        {
            return new Sensor();
        }

        public static Sensor Create(string text)
        {
            return Deserialize(text);
        }

        // Formato: Id;ZonaId;Tipo;X;Y;Activo
        public static Sensor Create(long id, long zona, TipoSensor tipo, double x, double y, bool activo = true)
        {
            return new Sensor 
            { 
                Id = id, 
                _zonaId = zona, 
                Tipo = tipo, 
                x = x, 
                y = y, 
                Activo = activo 
            };
        }

        private const int campos = 6; // const = no permite cambiar el valor 

        public static string Serialize(Sensor sensor)
        {
            return Csv.Unir(sensor.Id, sensor.ZonaId, sensor.Tipo, sensor.x, sensor.y, sensor.Activo); 
        }

        public static Sensor Deserialize(string text)
        {
            string[] campos = Csv.Partir(text, Sensor.campos);
            try
            {
                return Create(
                 
                    Csv.Long(campos[0]),
                    Csv.Long(campos[1]),
                    Csv.Enum<TipoSensor>(campos[2]),
                    Csv.Double(campos[3]),
                    Csv.Double(campos[4]),
                    Csv.Bool(campos[5])
                );
            }
            catch (Exception ex) when (
                ex is FormatException or OverflowException
                or ArgumentException or DomainExceptions)
            {
                throw new DatosCorruptosException(
                    "No se pudo leer el sensor", text, ex);
            }
        }



        public override string ToString()
        {
            return $"Sensor {Id} ({Tipo}) - Zona {ZonaId}{(Activo ? "" : " [inactivo]")}";
        }



    }

}
