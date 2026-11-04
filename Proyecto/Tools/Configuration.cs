using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Tools
{
    internal static class Configuration
    {
        public static readonly string Carpeta = Path.Combine(AppContext.BaseDirectory, "Datos");

        public static string ArchivoZonas { 
            get 
            { 
                return Path.Combine(Carpeta, "zonas.csv"); 
            } 
        }
        public static string ArchivoSensores { 
            get 
            { 
                return Path.Combine(Carpeta, "sensores.csv"); 
            } 
        }
        public static string ArchivoLecturas { 
            get 
            { 
                return Path.Combine(Carpeta, "lecturas.csv"); 
            } 
        }
    }

}
