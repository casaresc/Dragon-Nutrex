using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Models
{
    public class ResumenDiario
    {
        public decimal CaloriasConsumidas { get; set; }

        public decimal CarbohidratosConsumidos { get; set; }

        public decimal ProteinasConsumidas { get; set; }

        public decimal GrasasConsumidas { get; set; }

        public decimal MetaCalorias { get; set; }

        public decimal DiferenciaCalorias { get; set; }

        public bool TieneRegistros { get; set; }
    }
}
