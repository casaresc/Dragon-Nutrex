using Dragon_Nutrex_Web.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dragon_Nutrex_Web.Core.Models
{
    public class Producto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Nombre { get; set; }

        public CategoriaProducto Categoria { get; set; }

        public decimal Calorias { get; set; }
        [Range(0, 1000, ErrorMessage = "Proteína inválida")]
        public decimal Proteina { get; set; }
        [Range(0, 1000, ErrorMessage = "Carbohidratos inválidos")]
        public decimal Carbohidratos { get; set; }
        [Range(0, 1000, ErrorMessage = "Grasas inválidas")]
        public decimal Grasas { get; set; }
        [Range(1, 2000, ErrorMessage = "Porción inválida")]
        public decimal PorcionGramos { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }
    }
}
