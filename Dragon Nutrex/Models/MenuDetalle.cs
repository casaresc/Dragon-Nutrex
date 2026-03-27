using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Models
{
    public class MenuDetalle
    {
        public Guid Id { get; set; }

        public Guid MenuId { get; set; }

        public Guid ProductoId { get; set; }

        public decimal Porcion { get; set; }

        public bool Activo { get; set; } = true;
    }
}
