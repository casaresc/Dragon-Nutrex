using System.Text;

namespace Dragon_Nutrex_Web.Common
{
    /// <summary>
    /// Proporciona funciones de registro de errores en archivo local.
    /// </summary>
    public static class Logger
    {
        private static readonly string logDirectory =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        private static readonly string logPath =
            Path.Combine(logDirectory, "logs.txt");

        /// <summary>
        /// Registra una excepción en el archivo de logs.
        /// </summary>
        /// <param name="exception">Excepción a registrar.</param>
        /// <param name="contexto">Contexto opcional donde ocurrió el error.</param>
        public static void Log(Exception exception, string? contexto = null)
        {
            try
            {
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("==================================================");
                stringBuilder.AppendLine($"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                stringBuilder.AppendLine($"Contexto: {contexto ?? "No especificado"}");
                stringBuilder.AppendLine($"Tipo: {exception.GetType().FullName}");
                stringBuilder.AppendLine($"Mensaje: {exception.Message}");

                if (exception.InnerException is not null)
                {
                    stringBuilder.AppendLine($"InnerException: {exception.InnerException.Message}");
                }

                stringBuilder.AppendLine("StackTrace:");
                stringBuilder.AppendLine(exception.StackTrace ?? "No disponible");
                stringBuilder.AppendLine("==================================================");
                stringBuilder.AppendLine();

                File.AppendAllText(logPath, stringBuilder.ToString());
            }
            catch
            {
                // Evita que falle la aplicación por un error al escribir logs.
            }
        }
    }
}