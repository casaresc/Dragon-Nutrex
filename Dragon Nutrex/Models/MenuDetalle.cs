using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Models
{
    public class MenuDetalle
    {
        public Guid Id { get; set; }

        public Guid ProductoId { get; set; }

        public string? ProductoNombre { get; set; }

        public decimal CantidadGramos { get; set; }

        public TipoComida TipoComida { get; set; }
    }
}
