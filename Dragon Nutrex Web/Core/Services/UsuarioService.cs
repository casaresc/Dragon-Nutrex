using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Repositories;

namespace Dragon_Nutrex_Web.Core.Services
{
    public class UsuarioService
    {
        private readonly IRepository<Usuario> _usuarioRepository = new UsuarioRepository();

        public List<Usuario> ObtenerTodos()
        {
            return _usuarioRepository.GetAll();
        }

        public void CrearUsuario(Usuario usuario)
        {
            ValidarUsuario(usuario);
            if (usuario.Id == Guid.Empty)
            {
                usuario.Id = Guid.NewGuid();
            }
            _usuarioRepository.Create(usuario);
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            ValidarUsuario(usuario);
            _usuarioRepository.Update(usuario);
        }

        public void EliminarUsuario(Guid id)
        {
            _usuarioRepository.Delete(id);
        }

        public Usuario? ObtenerPorId(Guid id)
        {
            return _usuarioRepository.GetById(id);
        }

        private static void ValidarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("El nombre del usuario es obligatorio");

            if (usuario.Peso <= 0)
                throw new Exception("El peso debe ser mayor a 0");

            if (usuario.Altura <= 0)
                throw new Exception("La altura debe ser mayor a 0");

            if (usuario.Edad <= 0)
                throw new Exception("La edad debe ser válida");
        }
    }
}