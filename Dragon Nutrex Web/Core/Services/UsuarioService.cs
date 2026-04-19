using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Services
{
    public class UsuarioService
    {
        private readonly IRepository<Usuario> usuarioRepository;

        public UsuarioService(IRepository<Usuario> usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public List<Usuario> ObtenerTodos()
        {
            return usuarioRepository.GetAll();
        }

        public void CrearUsuario(Usuario usuario)
        {
            ValidarUsuario(usuario);

            if (usuario.Id == Guid.Empty)
            {
                usuario.Id = Guid.NewGuid();
            }

            usuarioRepository.Create(usuario);
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            ValidarUsuario(usuario);
            usuarioRepository.Update(usuario);
        }

        public void EliminarUsuario(Guid id)
        {
            usuarioRepository.Delete(id);
        }

        public Usuario? ObtenerPorId(Guid id)
        {
            return usuarioRepository.GetById(id);
        }

        private static void ValidarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("El nombre del usuario es obligatorio");

            if (string.IsNullOrWhiteSpace(usuario.Correo))
                throw new Exception("El correo del usuario es obligatorio");

            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                throw new Exception("La contraseña del usuario es obligatoria");

            if (usuario.Peso <= 0)
                throw new Exception("El peso debe ser mayor a 0");

            if (usuario.Altura <= 0)
                throw new Exception("La altura debe ser mayor a 0");

            if (usuario.Edad <= 0)
                throw new Exception("La edad debe ser válida");
        }
    }
}