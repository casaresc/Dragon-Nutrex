using Dragon_Nutrex.Models;
using Dragon_Nutrex.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Controllers
{
    public class UsuarioController
    {
        private UsuarioService service = new UsuarioService();

        public void CrearUsuario(Usuario usuario)
        {
            service.CrearUsuario(usuario);
        }
    }
}
