using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Gestiona la autenticación básica del sistema.
    /// </summary>
    public class AuthService
    {
        private readonly UsuarioService usuarioService;

        public AuthService(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        public SesionUsuario? SesionActual { get; private set; }

        /// <summary>
        /// Intenta autenticar un usuario por correo y contraseña.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="contrasena">Contraseña del usuario.</param>
        /// <returns>Verdadero si la autenticación fue exitosa.</returns>
        public bool IniciarSesion(string correo, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                return false;
            }

            var usuario = usuarioService.ObtenerTodos()
                .FirstOrDefault(u =>
                    u.Correo.Equals(correo, StringComparison.OrdinalIgnoreCase) &&
                    u.Contrasena == contrasena &&
                    u.Activo);

            if (usuario is null)
            {
                return false;
            }

            SesionActual = new SesionUsuario
            {
                UsuarioId = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            };

            return true;
        }

        /// <summary>
        /// Cierra la sesión actual.
        /// </summary>
        public void CerrarSesion()
        {
            SesionActual = null;
        }

        /// <summary>
        /// Indica si existe una sesión autenticada.
        /// </summary>
        public bool EstaAutenticado()
        {
            return SesionActual is not null && SesionActual.EstaAutenticado;
        }
    }
}