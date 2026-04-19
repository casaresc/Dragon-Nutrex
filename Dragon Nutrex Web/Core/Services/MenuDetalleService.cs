using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Repositories;

namespace Dragon_Nutrex_Web.Core.Services
{
    public class MenuDetalleService
    {
        private readonly IRepository<MenuDetalle> detalleRepository;

        public MenuDetalleService(IRepository<MenuDetalle> detalleRepository)
        {
            this.detalleRepository = detalleRepository;
        }

        public List<MenuDetalle> ObtenerTodos()
        {
            return detalleRepository.GetAll();
        }

        public List<MenuDetalle> ObtenerPorMenu(Guid menuId)
        {
            return ((MenuDetalleRepository)detalleRepository).GetByMenu(menuId);
        }

        public void AgregarProducto(MenuDetalle detalle)
        {
            ValidarDetalle(detalle);

            if (detalle.Id == Guid.Empty)
            {
                detalle.Id = Guid.NewGuid();
            }

            detalleRepository.Create(detalle);
        }

        public void ActualizarDetalle(MenuDetalle detalle)
        {
            ValidarDetalle(detalle);
            detalleRepository.Update(detalle);
        }

        public void EliminarProducto(Guid detalleId)
        {
            detalleRepository.Delete(detalleId);
        }

        private static void ValidarDetalle(MenuDetalle detalle)
        {
            if (detalle.MenuId == Guid.Empty)
                throw new Exception("El detalle debe estar asociado a un menú.");

            if (detalle.ProductoId == Guid.Empty)
                throw new Exception("El detalle debe estar asociado a un producto.");

            if (detalle.Porcion <= 0)
                throw new Exception("La porción debe ser mayor a cero.");
        }
    }
}