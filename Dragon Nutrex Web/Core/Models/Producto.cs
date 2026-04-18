using Dragon_Nutrex_Web.Core.Enums;

namespace Dragon_Nutrex_Web.Core.Models
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
