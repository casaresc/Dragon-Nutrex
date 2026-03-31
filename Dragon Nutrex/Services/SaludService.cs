namespace Dragon_Nutrex.Services
{
    public static class SaludService
    {
        public static decimal CalcularIMC(decimal peso, decimal altura)
        {
            if (altura <= 0)
                throw new ArgumentException("La altura debe ser mayor a cero.", nameof(altura));

            if (peso <= 0)
                throw new ArgumentException("El peso debe ser mayor a cero.", nameof(peso));

            return Math.Round(peso / (altura * altura), 2);
        }
    }
}