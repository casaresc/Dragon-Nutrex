using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Data
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public string Objetivo { get; set; }
        public string NivelActividad { get; set; }
    }
}
