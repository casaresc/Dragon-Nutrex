using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Gestiona la lógica de negocio relacionada con los detalles de menú.
    /// </summary>
    public class MenuDetalleService
    {
        private readonly IMenuDetalleRepository detalleRepository;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="MenuDetalleService"/>.
        /// </summary>
        /// <param name="detalleRepository">Repositorio de detalles de menú.</param>
        public MenuDetalleService(IMenuDetalleRepository detalleRepository)
        {
            this.detalleRepository = detalleRepository;
        }

        /// <summary>
        /// Obtiene todos los detalles de menú registrados.
        /// </summary>
        /// <returns>Lista de detalles de menú.</returns>
        public List<MenuDetalle> ObtenerTodos()
        {
            return detalleRepository.GetAll();
        }

        /// <summary>
        /// Obtiene los detalles asociados a un menú específico.
        /// </summary>
        /// <param name="menuId">Identificador del menú.</param>
        /// <returns>Lista de detalles asociados al menú.</returns>
        public List<MenuDetalle> ObtenerPorMenu(Guid menuId)
        {
            return detalleRepository.GetByMenu(menuId);
        }

        /// <summary>
        /// Agrega un producto al detalle de un menú.
        /// </summary>
        /// <param name="detalle">Detalle a registrar.</param>
        public void AgregarProducto(MenuDetalle detalle)
        {
            ValidarDetalle(detalle);

            if (detalle.Id == Guid.Empty)
            {
                detalle.Id = Guid.NewGuid();
            }

            detalleRepository.Create(detalle);
        }

        /// <summary>
        /// Actualiza un detalle de menú existente.
        /// </summary>
        /// <param name="detalle">Detalle a actualizar.</param>
        public void ActualizarDetalle(MenuDetalle detalle)
        {
            ValidarDetalle(detalle);
            detalleRepository.Update(detalle);
        }

        /// <summary>
        /// Elimina un detalle de menú por su identificador.
        /// </summary>
        /// <param name="detalleId">Identificador del detalle.</param>
        public void EliminarProducto(Guid detalleId)
        {
            detalleRepository.Delete(detalleId);
        }

        /// <summary>
        /// Valida las reglas de negocio del detalle de menú.
        /// </summary>
        /// <param name="detalle">Detalle a validar.</param>
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