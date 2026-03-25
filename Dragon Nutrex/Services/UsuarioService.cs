using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dragon_Nutrex.Services
{
    public class UsuarioService
    {
        private UsuarioRepository _usuarioRepository =
            new UsuarioRepository();

        public void CrearUsuario(Usuario usuario)
        {
            if (usuario.Peso <= 0)
                throw new Exception("Peso inválido");

            var usuarios = _usuarioRepository.GetAll();

            // Generar Id único
            usuario.Id = Guid.NewGuid();

            usuarios.Add(usuario);

            _usuarioRepository.SaveAll(usuarios);
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new Exception("Usuario inválido");

            if (usuario.Peso <= 0)
                throw new Exception("El peso debe ser mayor que cero");

            if (usuario.Altura <= 0)
                throw new Exception("La altura debe ser mayor que cero");

            _usuarioRepository.Update(usuario);
        }
    }
}