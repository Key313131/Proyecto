namespace Proyecto.Design
{
    // Un solo lugar para los colores de la interfaz (los mismos de la ventana Sesion).
    internal static class Tema
    {
        public static readonly Color Fondo = Color.FromArgb(3, 14, 32);
        public static readonly Color Tarjeta = Color.FromArgb(13, 34, 65);
        public static readonly Color Azul = Color.FromArgb(8, 86, 188);
        public static readonly Color Linea = Color.FromArgb(38, 62, 100);
        public static readonly Color Texto = Color.FromArgb(254, 255, 255);

        // La tabla se configura desde código y no en Designer.cs: el diseñador de Visual
        // Studio ya borró una vez las columnas y eso dejó colId en null.
        public static void Tabla(DataGridView tabla)
        {
            tabla.AutoGenerateColumns = false;
            tabla.AllowUserToAddRows = false;
            tabla.AllowUserToDeleteRows = false;
            tabla.AllowUserToResizeRows = false;
            tabla.ReadOnly = true;
            tabla.MultiSelect = false;
            tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabla.RowHeadersVisible = false;

            tabla.BorderStyle = BorderStyle.None;
            tabla.BackgroundColor = Fondo;
            tabla.GridColor = Linea;
            tabla.RowTemplate.Height = 36;

            // Sin esta línea Windows ignora los colores del encabezado.
            tabla.EnableHeadersVisualStyles = false;
            tabla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            tabla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tabla.ColumnHeadersHeight = 40;
            tabla.ColumnHeadersDefaultCellStyle.BackColor = Tarjeta;
            tabla.ColumnHeadersDefaultCellStyle.ForeColor = Texto;
            tabla.ColumnHeadersDefaultCellStyle.SelectionBackColor = Tarjeta;
            tabla.ColumnHeadersDefaultCellStyle.SelectionForeColor = Texto;

            tabla.DefaultCellStyle.BackColor = Fondo;
            tabla.DefaultCellStyle.ForeColor = Texto;
            tabla.DefaultCellStyle.SelectionBackColor = Azul;
            tabla.DefaultCellStyle.SelectionForeColor = Texto;
        }

        public static DataGridViewCheckBoxColumn ColumnaActivo()
        {
            DataGridViewCheckBoxColumn columna = new DataGridViewCheckBoxColumn();
            columna.Name = "colActivo";
            columna.DataPropertyName = "Activo";
            columna.HeaderText = "Activo";
            columna.ReadOnly = true;
            columna.Width = 65;
            return columna;
        }

        // ancho 0 significa que la columna ocupa todo el espacio libre.
        public static DataGridViewTextBoxColumn Columna(string propiedad, string titulo, int ancho)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = "col" + propiedad;
            columna.DataPropertyName = propiedad;
            columna.HeaderText = titulo;
            columna.ReadOnly = true;
            if (ancho == 0)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                columna.Width = ancho;
            }
            return columna;
        }
    }
}
