using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Models
{
    public class MenuDiario
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime Fecha { get; set; }

        public string? Nombre { get; set; }

        public List<MenuDetalle> Detalles { get; set; }
            = new List<MenuDetalle>();

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }
    }
}