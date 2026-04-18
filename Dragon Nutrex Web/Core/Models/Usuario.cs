using Dragon_Nutrex_Web.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dragon_Nutrex_Web.Core.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public required string Nombre { get; set; }
        [Range(1, 500, ErrorMessage = "El peso debe ser mayor a 0")]
        public required decimal Peso { get; set; }

        [Range(0.5, 3, ErrorMessage = "Altura inválida")]
        public required decimal Altura { get; set; }
        [Range(1, 120, ErrorMessage = "Edad inválida")]
        public required int Edad { get; set; }
        public required ObjetivoNutricional Objetivo { get; set; }
        public required NivelActividad NivelActividad { get; set; }
        public required TipoDieta TipoDieta { get; set; }
        public bool Activo { get; set; } = true;

    }
}