namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Proporciona cálculos básicos relacionados con salud.
    /// </summary>
    public static class SaludService
    {
        /// <summary>
        /// Calcula el Índice de Masa Corporal (IMC).
        /// </summary>
        /// <param name="peso">Peso en kilogramos.</param>
        /// <param name="altura">Altura en metros.</param>
        /// <returns>Valor del IMC redondeado a dos decimales.</returns>
        public static decimal CalcularIMC(decimal peso, decimal altura)
        {
            ValidarParametros(peso, altura);

            var imc = peso / (altura * altura);
            return Math.Round(imc, 2);
        }

        /// <summary>
        /// Valida los parámetros necesarios para el cálculo del IMC.
        /// </summary>
        /// <param name="peso">Peso en kilogramos.</param>
        /// <param name="altura">Altura en metros.</param>
        private static void ValidarParametros(decimal peso, decimal altura)
        {
            if (peso <= 0)
            {
                throw new ArgumentException("El peso debe ser mayor a cero.", nameof(peso));
            }

            if (altura <= 0)
            {
                throw new ArgumentException("La altura debe ser mayor a cero.", nameof(altura));
            }
        }
    }
}