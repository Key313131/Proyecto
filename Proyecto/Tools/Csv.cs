using Proyecto.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Proyecto.Tools
{
    internal static class Csv
    {
        public const char Separador = ';';

        private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        public static string Unir(params object[] valores)
        {
            return string.Join(Separador, valores.Select(v => Convert.ToString(v, Inv)));
        }

        public static string[] Partir(string linea, int camposEsperados)
        {
            if (string.IsNullOrWhiteSpace(linea))
            {
                throw new DatosCorruptosException("Línea vacía", linea);
            }
            
            string[] campos = linea.Split(Separador);

            if(campos.Length != camposEsperados)
            {
                throw new DatosCorruptosException(
                    $"Se esperaban {camposEsperados} campos y hay {campos.Length}", linea);
            }

            return campos;

        }

        public static long Long(string s)
        {
            return long.Parse(s, Inv);
        }
        public static double Double(string s) => double.Parse(s, Inv);
        public static bool Bool(string s) => bool.Parse(s);
        public static DateTime Fecha(string s) => DateTime.Parse(s, Inv);
        public static string Fecha(DateTime f) => f.ToString("yyyy-MM-dd HH:mm:ss", Inv);

        public static TEnum Enum<TEnum>(string s) where TEnum : struct, System.Enum
        {


            TEnum valor = System.Enum.Parse<TEnum>(s);
            if (!System.Enum.IsDefined(valor)) 
            {
                throw new FormatException($"'{s}' no es un valor válido de {typeof(TEnum).Name}");
            }
                
            return valor;
        }

    }

}
