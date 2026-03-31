using System;
using System.Windows.Forms;

namespace Dragon_Nutrex.Common
{
    public static class GlobalExceptionHandler
    {
        public static void Handle(Exception ex)
        {
            Logger.Log(ex);

            MessageBox.Show(
                "Ocurrió un error inesperado.\n" +
                "Por favor intente nuevamente y revise los logs para ver más detalles.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}