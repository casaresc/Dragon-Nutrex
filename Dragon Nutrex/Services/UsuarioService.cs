using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;
using System;
using System.Collections.Generic;

namespace Dragon_Nutrex.Services
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