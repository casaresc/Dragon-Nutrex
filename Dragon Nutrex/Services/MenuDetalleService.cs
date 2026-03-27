using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Services
{
    public class MenuDetalleService
    {
        private readonly MenuDetalleRepository _repository;

        public MenuDetalleService()
        {
            _repository =
                new MenuDetalleRepository();
        }

        public List<MenuDetalle> ObtenerPorMenu(
            Guid menuId)
        {
            return _repository
                .ObtenerPorMenu(menuId);
        }

        public void AgregarProducto(
            MenuDetalle detalle)
        {
            ValidarDetalle(detalle);

            _repository.Agregar(detalle);
        }

        public void EliminarProducto(
            Guid id)
        {
            _repository.Eliminar(id);
        }

        private void ValidarDetalle(
            MenuDetalle detalle)
        {
            if (detalle.MenuId == Guid.Empty)
                throw new ArgumentException(
                    "Menú inválido.");

            if (detalle.ProductoId == Guid.Empty)
                throw new ArgumentException(
                    "Producto inválido.");

            if (detalle.Porcion <= 0)
                throw new ArgumentException(
                    "La porción debe ser mayor a cero.");
        }
    }
}
