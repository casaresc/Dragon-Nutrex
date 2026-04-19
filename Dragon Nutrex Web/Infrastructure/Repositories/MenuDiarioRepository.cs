using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio SQL Server para menús diarios.
    /// </summary>
    public class MenuDiarioRepository : IRepository<MenuDiario>
    {
        private readonly SqlConnectionFactory connectionFactory;

        public MenuDiarioRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public List<MenuDiario> GetAll()
        {
            var menus = new List<MenuDiario>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    UsuarioId,
                    Nombre,
                    Fecha,
                    Activo
                FROM MenusDiarios;";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                menus.Add(MapMenu(reader));
            }

            return menus;
        }

        public MenuDiario? GetById(Guid id)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    UsuarioId,
                    Nombre,
                    Fecha,
                    Activo
                FROM MenusDiarios
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapMenu(reader);
            }

            return null;
        }

        public void Create(MenuDiario menu)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                INSERT INTO MenusDiarios (
                    Id,
                    UsuarioId,
                    Nombre,
                    Fecha,
                    Activo,
                    FechaCreacion
                )
                VALUES (
                    @Id,
                    @UsuarioId,
                    @Nombre,
                    @Fecha,
                    @Activo,
                    SYSDATETIME()
                );";

            using var command = new SqlCommand(query, connection);
            AddCommonParameters(command, menu);

            command.ExecuteNonQuery();
        }

        public void Update(MenuDiario menu)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                UPDATE MenusDiarios
                SET
                    UsuarioId = @UsuarioId,
                    Nombre = @Nombre,
                    Fecha = @Fecha,
                    Activo = @Activo,
                    FechaActualizacion = SYSDATETIME()
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            AddCommonParameters(command, menu);

            command.ExecuteNonQuery();
        }

        public void Delete(Guid id)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"DELETE FROM MenusDiarios WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        public MenuDiario? GetByUsuarioYFecha(Guid usuarioId, DateTime fecha)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    UsuarioId,
                    Nombre,
                    Fecha,
                    Activo
                FROM MenusDiarios
                WHERE UsuarioId = @UsuarioId
                  AND Fecha = @Fecha;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UsuarioId", usuarioId);
            command.Parameters.AddWithValue("@Fecha", fecha.Date);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapMenu(reader);
            }

            return null;
        }

        private static void AddCommonParameters(SqlCommand command, MenuDiario menu)
        {
            command.Parameters.AddWithValue("@Id", menu.Id);
            command.Parameters.AddWithValue("@UsuarioId", menu.UsuarioId);
            command.Parameters.AddWithValue("@Nombre", menu.Nombre);
            command.Parameters.AddWithValue("@Fecha", menu.Fecha.Date);
            command.Parameters.AddWithValue("@Activo", menu.Activo);
        }

        private static MenuDiario MapMenu(SqlDataReader reader)
        {
            return new MenuDiario
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                UsuarioId = reader.GetGuid(reader.GetOrdinal("UsuarioId")),
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Fecha = Convert.ToDateTime(reader["Fecha"]),
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }
    }
}