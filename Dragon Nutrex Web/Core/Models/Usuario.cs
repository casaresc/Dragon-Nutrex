using Dragon_Nutrex_Web.Core.Enums;

namespace Dragon_Nutrex_Web.Core.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }

        public required decimal Peso { get; set; }
        public required decimal Altura { get; set; }
        public required int Edad { get; set; }

        public required ObjetivoNutricional Objetivo { get; set; }
        public required NivelActividad NivelActividad { get; set; }
        public required TipoDieta TipoDieta { get; set; }

        public bool Activo { get; set; } = true;

    }
}