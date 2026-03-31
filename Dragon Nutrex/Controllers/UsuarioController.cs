using Dragon_Nutrex.Common;
using Dragon_Nutrex.Models;
using Dragon_Nutrex.Services;
using System;
using System.Collections.Generic;

namespace Dragon_Nutrex.Controllers
{
    public class UsuarioController
    {
        private readonly UsuarioService _usuarioService = new UsuarioService();

        public List<Usuario> GetUsuarios()
        {
            try
            {
                return _usuarioService.ObtenerTodos();
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return new List<Usuario>();
            }
        }
        public Usuario? ObtenerPorId(Guid id)
        {
            try
            {
                return _usuarioService.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return null;
            }
        }
        public void CrearUsuario(Usuario usuario)
        {
            try
            {
                _usuarioService.CrearUsuario(usuario);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
            }
        }
        public void ActualizarUsuario(Usuario usuario)
        {
            try
            {
                _usuarioService.ActualizarUsuario(usuario);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
            }
        }
        public void EliminarUsuario(Guid id)
        {
            try
            {
                _usuarioService.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
            }
        }
        public List<Usuario> ObtenerUsuariosActivos()
        {
            try { return _usuarioService.ObtenerTodos(); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); return new List<Usuario>(); }
        }
    }
}