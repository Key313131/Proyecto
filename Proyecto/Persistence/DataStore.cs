using Proyecto.Contracts;
using Proyecto.DynamicStructure;
using System.Text;

namespace Proyecto.Persistence
{
    internal class DataStore<T>: IDataStore<T> where T : class, IFactory<T>, ISerialize<T>
    {
        private readonly string _path;

        public DataStore(string path)
        {
            _path = path;
        }

        public Lista<T> LoadAll()
        {
            Lista<T> list = new Lista<T>();
            
            if (!File.Exists(_path))
            {
                return list;
            }

            using (StreamReader sr = new StreamReader(_path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    // Una linea dañada lanza DatosCorruptosException no se atrapa aqui
                    // ignorarla y guardar despues borraria ese dato sin avisar
                    list.Add(T.Create(line));
                }
            }

            return list;
        }

        //  ==================================================

        public void SaveAll(IEnumerable<T> list)
        {
            StringBuilder sb = new StringBuilder();

            foreach (T item in list)
            {
                sb.AppendLine(T.Serialize(item));
            }

            string? carpeta = Path.GetDirectoryName(_path);
            if (!string.IsNullOrEmpty(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            // Se escribe primero en un temporal y luego se reemplaza el original
            // Si algo falla a mitad de camino, el archivo original queda intacto
            string temporal = _path + ".tmp";

            using (StreamWriter sw = new StreamWriter(temporal))
            {
                sw.Write(sb.ToString());
            }

            File.Move(temporal, _path, overwrite: true);
        }
    }
}
