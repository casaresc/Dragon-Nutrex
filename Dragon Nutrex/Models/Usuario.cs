using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Models
{
    public class Usuario
    {
        
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required double Peso { get; set; }
        public required double Altura { get; set; }
        public required string Objetivo { get; set; }
        public required string NivelActividad { get; set; }
        public required string TipoDieta { get; set; }
    }
}
