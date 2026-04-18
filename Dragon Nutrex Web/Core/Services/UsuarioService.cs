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
            if (usuario.Id == Guid.Empty)
            {
                usuario.Id = Guid.NewGuid();
            }
            _usuarioRepository.Create(usuario);
        }

        public void ActualizarUsuario(Usuario usuario)
        {
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
    }
}