using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio SQL Server para detalles de menú.
    /// </summary>
    public class MenuDetalleRepository : IRepository<MenuDetalle>
    {
        private readonly SqlConnectionFactory connectionFactory;

        public MenuDetalleRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public List<MenuDetalle> GetAll()
        {
            var detalles = new List<MenuDetalle>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    MenuId,
                    ProductoId,
                    Porcion
                FROM MenuDetalles;";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                detalles.Add(MapDetalle(reader));
            }

            return detalles;
        }

        public MenuDetalle? GetById(Guid id)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    MenuId,
                    ProductoId,
                    Porcion
                FROM MenuDetalles
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapDetalle(reader);
            }

            return null;
        }

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

        public void Delete(Guid id)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"DELETE FROM MenuDetalles WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        public List<MenuDetalle> GetByMenu(Guid menuId)
        {
            var detalles = new List<MenuDetalle>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    MenuId,
                    ProductoId,
                    Porcion
                FROM MenuDetalles
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

        private static void AddCommonParameters(SqlCommand command, MenuDetalle detalle)
        {
            command.Parameters.AddWithValue("@Id", detalle.Id);
            command.Parameters.AddWithValue("@MenuId", detalle.MenuId);
            command.Parameters.AddWithValue("@ProductoId", detalle.ProductoId);
            command.Parameters.AddWithValue("@Porcion", detalle.Porcion);
        }

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