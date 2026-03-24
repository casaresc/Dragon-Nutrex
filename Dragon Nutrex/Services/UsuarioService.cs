using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Services
{
    public class UsuarioService
    {
        private UsuarioRepository repo = new UsuarioRepository();

        public void CrearUsuario(Usuario usuario)
        {
            if (usuario.Peso <= 0)
                throw new Exception("Peso inválido");

            var usuarios = repo.GetAll();

            usuario.Id = GenerarId(usuarios);

            usuarios.Add(usuario);

            repo.SaveAll(usuarios);
        }

        private int GenerarId(List<Usuario> usuarios)
        {
            if (usuarios.Count == 0)
                return 1;

            return usuarios.Max(u => u.Id) + 1;
        }
    }
}
