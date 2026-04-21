namespace Dragon_Nutrex_Web.Common
{
    /// <summary>
    /// Centraliza el manejo global de excepciones de la aplicación.
    /// </summary>
    public static class GlobalExceptionHandler
    {
        private const string MensajeErrorGenerico =
            "Ocurrió un error inesperado. Revisa los logs para más detalles.";

        /// <summary>
        /// Registra una excepción en el sistema de logs.
        /// </summary>
        /// <param name="exception">Excepción capturada.</param>
        /// <param name="contexto">Contexto opcional donde ocurrió el error.</param>
        public static void Handle(Exception exception, string? contexto = null)
        {
            Logger.Log(exception, contexto);
        }

        /// <summary>
        /// Registra una excepción y devuelve un mensaje amigable para mostrar en pantalla.
        /// </summary>
        /// <param name="exception">Excepción capturada.</param>
        /// <param name="contexto">Contexto opcional donde ocurrió el error.</param>
        /// <returns>Mensaje amigable para el usuario.</returns>
        public static string HandleWithMessage(Exception exception, string? contexto = null)
        {
            Logger.Log(exception, contexto);
            return MensajeErrorGenerico;
        }
    }
}