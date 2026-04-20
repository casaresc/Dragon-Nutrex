using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Gestiona la lógica de negocio relacionada con usuarios.
    /// </summary>
    public class UsuarioService
    {
        private const string ContrasenaTemporalPorDefecto = "1234";

        private readonly IRepository<Usuario> usuarioRepository;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="UsuarioService"/>.
        /// </summary>
        /// <param name="usuarioRepository">Repositorio de persistencia de usuarios.</param>
        public UsuarioService(IRepository<Usuario> usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        public List<Usuario> ObtenerTodos()
        {
            return usuarioRepository.GetAll();
        }

        /// <summary>
        /// Obtiene un usuario por su identificador.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <returns>Usuario encontrado o null.</returns>
        public Usuario? ObtenerPorId(Guid usuarioId)
        {
            return usuarioRepository.GetById(usuarioId);
        }

        /// <summary>
        /// Crea un nuevo usuario aplicando las validaciones de negocio.
        /// </summary>
        /// <param name="usuario">Usuario a registrar.</param>
        public void CrearUsuario(Usuario usuario)
        {
            ValidarUsuario(usuario);

            if (usuario.Id == Guid.Empty)
            {
                usuario.Id = Guid.NewGuid();
            }

            usuarioRepository.Create(usuario);
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="usuario">Usuario a actualizar.</param>
        public void ActualizarUsuario(Usuario usuario)
        {
            ValidarUsuario(usuario);
            usuarioRepository.Update(usuario);
        }

        /// <summary>
        /// Elimina un usuario por su identificador.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        public void EliminarUsuario(Guid usuarioId)
        {
            usuarioRepository.Delete(usuarioId);
        }

        /// <summary>
        /// Restablece la contraseña de un usuario a un valor temporal por defecto.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        public void ResetearContrasena(Guid usuarioId)
        {
            var usuario = ObtenerUsuarioExistente(usuarioId);
            usuario.Contrasena = ContrasenaTemporalPorDefecto;

            usuarioRepository.Update(usuario);
        }

        /// <summary>
        /// Desactiva un usuario del sistema.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        public void DesactivarUsuario(Guid usuarioId)
        {
            var usuario = ObtenerUsuarioExistente(usuarioId);
            usuario.Activo = false;

            usuarioRepository.Update(usuario);
        }

        /// <summary>
        /// Activa un usuario del sistema.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        public void ActivarUsuario(Guid usuarioId)
        {
            var usuario = ObtenerUsuarioExistente(usuarioId);
            usuario.Activo = true;

            usuarioRepository.Update(usuario);
        }

        /// <summary>
        /// Obtiene un usuario existente o lanza una excepción si no existe.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <returns>Usuario existente.</returns>
        private Usuario ObtenerUsuarioExistente(Guid usuarioId)
        {
            return usuarioRepository.GetById(usuarioId)
                ?? throw new Exception("Usuario no encontrado");
        }

        /// <summary>
        /// Valida las reglas de negocio requeridas para un usuario.
        /// </summary>
        /// <param name="usuario">Usuario a validar.</param>
        private static void ValidarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("El nombre del usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Correo))
                throw new Exception("El correo del usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                throw new Exception("La contraseña del usuario es obligatoria.");

            if (usuario.Peso <= 0)
                throw new Exception("El peso debe ser mayor a 0.");

            if (usuario.Altura <= 0)
                throw new Exception("La altura debe ser mayor a 0.");

            if (usuario.Edad <= 0)
                throw new Exception("La edad debe ser válida.");
        }
    }
}