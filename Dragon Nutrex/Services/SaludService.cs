using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Services
{
    namespace Dragon_Nutrex.Services
    {
        public class SaludService
        {
            public decimal CalcularIMC(
                decimal peso,
                decimal altura)
            {
                if (altura <= 0)
                    throw new ArgumentException(
                        "La altura debe ser mayor a cero.");

                if (peso <= 0)
                    throw new ArgumentException(
                        "El peso debe ser mayor a cero.");

                var imc =
                    peso / (altura * altura);

                return Math.Round(
                    imc,
                    2);
            }
        }
    }
}
