using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Common
{
    public static class GlobalExceptionHandler
    {
        public static void Handle(Exception ex)
        {
            // Mensaje amigable para el usuario
            MessageBox.Show(
                $"Ocurrió un error:\n\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
