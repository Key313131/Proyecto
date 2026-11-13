namespace Proyecto.Presentacion
{
    internal static class UiHelper
    {

        public static void Poner(NumericUpDown control, double valor)
        {
            double ajustado = Math.Clamp(valor, (double)control.Minimum, (double)control.Maximum);
            control.Value = (decimal)ajustado;
        }

        public static void Aviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Revise los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
