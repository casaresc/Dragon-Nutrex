namespace Dragon_Nutrex_Web.Core.Models
{
    public class UsuarioMenusResultado
    {
        public Guid UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public int CantidadMenus { get; set; }
    }
}