using Dragon_Nutrex.Common;
using Dragon_Nutrex.Models;
using Dragon_Nutrex.Services;
using System;
using System.Collections.Generic;

namespace Dragon_Nutrex.Controllers
{
    public class ProductoController
    {
        private readonly ProductoService _productoService = new ProductoService();

        public List<Producto> ObtenerTodos()
        {
            try { return _productoService.ObtenerProductos(); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); return new List<Producto>(); }
        }

        public void Crear(Producto p)
        {
            try { _productoService.CrearProducto(p); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); }
        }

        public void Actualizar(Producto p)
        {
            try { _productoService.ActualizarProducto(p); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); }
        }

        public void Eliminar(Guid id)
        {
            try { _productoService.EliminarProducto(id); }
            catch (Exception ex) { GlobalExceptionHandler.Handle(ex); }
        }
        public List<Producto> GetProductos()
        {
            try
            {
                return _productoService.ObtenerTodos();
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
                return new List<Producto>();
            }
        }

        public void GuardarProducto(Producto producto)
        {
            try
            {
                // Si el Id es Empty, es un producto nuevo
                if (producto.Id == Guid.Empty)
                {
                    _productoService.CrearProducto(producto);
                }
                else
                {
                    _productoService.ActualizarProducto(producto);
                }
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
            }
        }

        public void EliminarProducto(Guid id)
        {
            try
            {
                _productoService.EliminarProducto(id);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler.Handle(ex);
            }
        }
    }
}