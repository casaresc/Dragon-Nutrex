using System;
using System.Windows.Forms;

namespace Dragon_Nutrex.Common
{
    public static class GlobalExceptionHandler
    {
        public static void Handle(Exception ex)
        {
            // Registrar error
            Logger.Log(ex);

            // Mostrar mensaje amigable
            MessageBox.Show(
                "Ocurrió un error inesperado.\n" +
                "Por favor intente nuevamente.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}