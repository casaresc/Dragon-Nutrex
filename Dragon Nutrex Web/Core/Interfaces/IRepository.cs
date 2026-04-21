namespace Dragon_Nutrex_Web.Core.Interfaces
{
    /// <summary>
    /// Define operaciones básicas de persistencia para entidades.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Obtiene todos los registros.
        /// </summary>
        List<T> GetAll();

        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        T? GetById(Guid id);

        /// <summary>
        /// Crea una nueva entidad.
        /// </summary>
        /// <param name="entity">Entidad a crear.</param>
        void Create(T entity);

        /// <summary>
        /// Actualiza una entidad existente.
        /// </summary>
        /// <param name="entity">Entidad a actualizar.</param>
        void Update(T entity);

        /// <summary>
        /// Elimina una entidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        void Delete(Guid id);
    }
}