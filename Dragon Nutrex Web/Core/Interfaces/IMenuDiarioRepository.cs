using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Interfaces
{
    /// <summary>
    /// Define operaciones específicas para la entidad MenuDiario.
    /// </summary>
    public interface IMenuDiarioRepository : IRepository<MenuDiario>
    {
        /// <summary>
        /// Obtiene un menú por usuario y fecha específica.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <param name="fecha">Fecha del menú.</param>
        /// <returns>Menú encontrado o null si no existe.</returns>
        MenuDiario? GetByUsuarioYFecha(Guid usuarioId, DateTime fecha);
    }
}