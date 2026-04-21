using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio SQL Server para la persistencia de detalles de menú.
    /// </summary>
    public class MenuDetalleRepository : IMenuDetalleRepository
    {
        private const string SelectMenuDetallesQuery = @"
            SELECT
                Id,
                MenuId,
                ProductoId,
                Porcion
            FROM MenuDetalles";

        private readonly SqlConnectionFactory connectionFactory;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="MenuDetalleRepository"/>.
        /// </summary>
        /// <param name="connectionFactory">Fábrica de conexiones SQL.</param>
        public MenuDetalleRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Obtiene todos los detalles de menú registrados.
        /// </summary>
        /// <returns>Lista de detalles de menú.</returns>
        public List<MenuDetalle> GetAll()
        {
            var detalles = new List<MenuDetalle>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            using var command = new SqlCommand(SelectMenuDetallesQuery + ";", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                detalles.Add(MapDetalle(reader));
            }

            return detalles;
        }

        /// <summary>
        /// Obtiene un detalle de menú por su identificador.
        /// </summary>
        /// <param name="detalleId">Identificador del detalle.</param>
        /// <returns>Detalle encontrado o null.</returns>
        public MenuDetalle? GetById(Guid detalleId)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = SelectMenuDetallesQuery + @"
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", detalleId);

            using var reader = command.ExecuteReader();

            return reader.Read() ? MapDetalle(reader) : null;
        }

        /// <summary>
        /// Crea un nuevo detalle de menú en la base de datos.
        /// </summary>
        /// <param name="detalle">Detalle a registrar.</param>
        public void Create(MenuDetalle detalle)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                INSERT INTO MenuDetalles (
                    Id,
                    MenuId,
                    ProductoId,
                    Porcion,
                    FechaCreacion
                )
                VALUES (
                    @Id,
                    @MenuId,
                    @ProductoId,
                    @Porcion,
                    SYSDATETIME()
                );";

            using var command = new SqlCommand(query, connection);
            AddCommonParameters(command, detalle);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Actualiza un detalle de menú existente en la base de datos.
        /// </summary>
        /// <param name="detalle">Detalle a actualizar.</param>
        public void Update(MenuDetalle detalle)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                UPDATE MenuDetalles
                SET
                    MenuId = @MenuId,
                    ProductoId = @ProductoId,
                    Porcion = @Porcion
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            AddCommonParameters(command, detalle);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Elimina un detalle de menú por su identificador.
        /// </summary>
        /// <param name="detalleId">Identificador del detalle.</param>
        public void Delete(Guid detalleId)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"DELETE FROM MenuDetalles WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", detalleId);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Obtiene todos los detalles asociados a un menú específico.
        /// </summary>
        /// <param name="menuId">Identificador del menú.</param>
        /// <returns>Lista de detalles asociados al menú.</returns>
        public List<MenuDetalle> GetByMenu(Guid menuId)
        {
            var detalles = new List<MenuDetalle>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = SelectMenuDetallesQuery + @"
                WHERE MenuId = @MenuId;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MenuId", menuId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                detalles.Add(MapDetalle(reader));
            }

            return detalles;
        }

        /// <summary>
        /// Agrega al comando SQL los parámetros comunes de detalle de menú.
        /// </summary>
        /// <param name="command">Comando SQL a completar.</param>
        /// <param name="detalle">Detalle fuente de datos.</param>
        private static void AddCommonParameters(SqlCommand command, MenuDetalle detalle)
        {
            command.Parameters.AddWithValue("@Id", detalle.Id);
            command.Parameters.AddWithValue("@MenuId", detalle.MenuId);
            command.Parameters.AddWithValue("@ProductoId", detalle.ProductoId);
            command.Parameters.AddWithValue("@Porcion", detalle.Porcion);
        }

        /// <summary>
        /// Mapea un registro de base de datos a una instancia de <see cref="MenuDetalle"/>.
        /// </summary>
        /// <param name="reader">Lector de datos SQL posicionado sobre un registro válido.</param>
        /// <returns>Instancia mapeada de detalle de menú.</returns>
        private static MenuDetalle MapDetalle(SqlDataReader reader)
        {
            return new MenuDetalle
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                MenuId = reader.GetGuid(reader.GetOrdinal("MenuId")),
                ProductoId = reader.GetGuid(reader.GetOrdinal("ProductoId")),
                Porcion = Convert.ToDecimal(reader["Porcion"])
            };
        }
    }
}