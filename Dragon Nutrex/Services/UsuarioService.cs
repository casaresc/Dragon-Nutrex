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

            var lista = repo.GetAll();
            lista.Add(usuario);
            repo.SaveAll(lista);
        }
    }
}
