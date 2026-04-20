using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Interfaces
{
    /// <summary>
    /// Define operaciones específicas para el repositorio de consumos diarios.
    /// </summary>
    public interface IConsumoDiarioRepository : IRepository<ConsumoDiario>
    {
        /// <summary>
        /// Obtiene los consumos registrados en una fecha específica.
        /// </summary>
        /// <param name="fecha">Fecha a consultar.</param>
        /// <returns>Lista de consumos de la fecha.</returns>
        List<ConsumoDiario> GetByDate(DateTime fecha);

        /// <summary>
        /// Obtiene los consumos registrados dentro de un rango de fechas.
        /// </summary>
        /// <param name="fechaInicio">Fecha inicial del rango.</param>
        /// <param name="fechaFin">Fecha final del rango.</param>
        /// <returns>Lista de consumos dentro del rango.</returns>
        List<ConsumoDiario> GetByRange(DateTime fechaInicio, DateTime fechaFin);
    }
}