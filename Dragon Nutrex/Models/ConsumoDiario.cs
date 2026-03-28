using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Models
{
    public class ConsumoDiario
    {
        public Guid Id { get; set; }

        public DateTime Fecha { get; set; }

        public decimal CaloriasConsumidas { get; set; }

        public decimal CarbohidratosConsumidos { get; set; }

        public bool Activo { get; set; } = true;
    }
}
