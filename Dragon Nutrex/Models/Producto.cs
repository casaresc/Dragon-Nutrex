using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Models
{
    public class Producto
    {
        public Guid Id { get; set; }

        public string? Nombre { get; set; }

        public CategoriaProducto Categoria { get; set; }

        public decimal Calorias { get; set; }

        public decimal Proteina { get; set; }

        public decimal Carbohidratos { get; set; }

        public decimal Grasas { get; set; }

        public decimal PorcionGramos { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }
    }
}
