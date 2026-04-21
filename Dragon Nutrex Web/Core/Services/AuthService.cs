using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Gestiona la autenticación básica del sistema y el estado de la sesión actual.
    /// </summary>
    public class AuthService
    {
        private readonly UsuarioService usuarioService;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="AuthService"/>.
        /// </summary>
        /// <param name="usuarioService">Servicio de usuarios utilizado para autenticar credenciales.</param>
        public AuthService(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        /// <summary>
        /// Obtiene la sesión actualmente autenticada.
        /// </summary>
        public SesionUsuario? SesionActual { get; private set; }

        /// <summary>
        /// Intenta autenticar un usuario por correo y contraseña.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="contrasena">Contraseña del usuario.</param>
        /// <returns>Verdadero si la autenticación fue exitosa; en caso contrario, falso.</returns>
        public bool IniciarSesion(string correo, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                return false;
            }

            var usuario = usuarioService.ObtenerTodos()
                .FirstOrDefault(usuario =>
                    usuario.Correo.Equals(correo, StringComparison.OrdinalIgnoreCase) &&
                    usuario.Contrasena == contrasena &&
                    usuario.Activo);

            if (usuario is null)
            {
                return false;
            }

            SesionActual = CrearSesionUsuario(usuario);
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
        /// Indica si existe una sesión autenticada actualmente.
        /// </summary>
        /// <returns>Verdadero si existe una sesión válida; en caso contrario, falso.</returns>
        public bool EstaAutenticado()
        {
            return SesionActual is not null && SesionActual.EstaAutenticado;
        }

        /// <summary>
        /// Crea una sesión de usuario a partir de un usuario autenticado.
        /// </summary>
        /// <param name="usuario">Usuario autenticado.</param>
        /// <returns>Sesión de usuario construida.</returns>
        private static SesionUsuario CrearSesionUsuario(Usuario usuario)
        {
            return new SesionUsuario
            {
                UsuarioId = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            };
        }
    }
}