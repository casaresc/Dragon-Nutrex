using Dragon_Nutrex.Common;
using Dragon_Nutrex.Models;
using Dragon_Nutrex.Services;
using System;
using System.Collections.Generic;

namespace Dragon_Nutrex.Controllers
{
    public class MenuController
    {
        private readonly MenuDiarioService _menuService = new MenuDiarioService();
        private readonly MenuDetalleService _detalleService = new MenuDetalleService();
        private readonly ProductoService _productoService = new ProductoService();
        private readonly MenuDiarioService _menuDiarioService = new MenuDiarioService();

        public void CrearPlanDiario(MenuDiario menu, List<MenuDetalle> detalles)
        {
            try
            {
                _menuService.CrearMenu(menu, detalles);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
            }
        }

        public List<MenuDiario> GetHistorialMenus()
        {
            try
            {
                return _menuService.ObtenerTodos();
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return new List<MenuDiario>();
            }
        }

        public List<Producto> ObtenerCatalogoProductos()
        {
            try { return _productoService.ObtenerProductos().Where(p => p.Activo).ToList(); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); return new List<Producto>(); }
        }

        public List<MenuDetalle> ObtenerDetallesDelMenu(Guid menuId)
        {
            try { return _detalleService.ObtenerPorMenu(menuId); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); return new List<MenuDetalle>(); }
        }

        public void AgregarAlimentoAlMenu(MenuDetalle detalle)
        {
            try { _detalleService.AgregarProducto(detalle); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); }
        }

        public void QuitarAlimentoDelMenu(Guid detalleId)
        {
            try { _detalleService.EliminarProducto(detalleId); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); }
        }

        public List<MenuDiario> ObtenerTodosLosMenus()
        {
            try { return _menuDiarioService.ObtenerMenus(); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); return new List<MenuDiario>(); }
        }

        public void GuardarNuevoMenu(MenuDiario menu)
        {
            try { _menuDiarioService.CrearMenu(menu); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); }
        }

        public void ModificarMenu(MenuDiario menu)
        {
            try { _menuDiarioService.ActualizarMenu(menu); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); }
        }

        public void BorrarMenu(Guid id)
        {
            try { _menuDiarioService.EliminarMenu(id); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); }
        }
    }
}