using Dragon_Nutrex.Models;
using Dragon_Nutrex.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Controllers
{
    public class UsuarioController
    {
        private UsuarioService _usuarioService = new UsuarioService();

        public void CrearUsuario(Usuario usuario)
        {
            _usuarioService.CrearUsuario(usuario);
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            try
            {
                _usuarioService.ActualizarUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario: " + ex.Message);
            }
        }
    }


}
