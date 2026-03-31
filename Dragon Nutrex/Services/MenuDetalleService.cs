using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;
using System;
using System.Collections.Generic;

namespace Dragon_Nutrex.Services
{
    public class MenuDetalleService
    {
        private readonly IRepository<MenuDetalle> _detalleRepository;

        public MenuDetalleService()
        {
            _detalleRepository = new MenuDetalleRepository();
        }

        public void GuardarDetalle(MenuDetalle detalle)
        {
            ValidarDetalle(detalle);
            _detalleRepository.Create(detalle);
        }

        public List<MenuDetalle> ObtenerPorMenu(Guid menuId)
        {
            if (menuId == Guid.Empty)
                return new List<MenuDetalle>();

            var repoConcreto = (MenuDetalleRepository)_detalleRepository;
            return repoConcreto.GetByMenuId(menuId);
        }

        public void EliminarDetalle(Guid id)
        {
            _detalleRepository.Delete(id);
        }

        private static void ValidarDetalle(MenuDetalle detalle)
        {
            if (detalle == null)
                throw new ArgumentNullException(nameof(detalle));

            if (detalle.ProductoId == Guid.Empty)
                throw new ArgumentException("El detalle debe estar asociado a un producto válido.", nameof(detalle));

            if (detalle.Porcion <= 0)
                throw new ArgumentException("La cantidad del alimento debe ser mayor a cero.", nameof(detalle));
        }

        public void AgregarProducto(MenuDetalle detalle)
        {
            GuardarDetalle(detalle);
        }

        public void EliminarProducto(Guid id)
        {
            EliminarDetalle(id);
        }
    }
}