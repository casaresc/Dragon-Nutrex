namespace Dragon_Nutrex_Web.Core.Models
{
    /// <summary>
    /// Representa la sesión activa del usuario autenticado.
    /// </summary>
    public class SesionUsuario
    {
        public Guid UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;

        public bool EstaAutenticado => UsuarioId != Guid.Empty;
        public bool EsAdmin => Rol == "Administrador";
    }
}