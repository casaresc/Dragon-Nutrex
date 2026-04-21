using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Interfaces
{
    /// <summary>
    /// Define operaciones específicas para el repositorio de detalles de menú.
    /// </summary>
    public interface IMenuDetalleRepository : IRepository<MenuDetalle>
    {
        /// <summary>
        /// Obtiene los detalles asociados a un menú.
        /// </summary>
        /// <param name="menuId">Identificador del menú.</param>
        /// <returns>Lista de detalles del menú.</returns>
        List<MenuDetalle> GetByMenu(Guid menuId);
    }
}