using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio SQL Server para la persistencia de menús diarios.
    /// </summary>
    public class MenuDiarioRepository : IMenuDiarioRepository
    {
        private const string SelectMenusQuery = @"
            SELECT
                Id,
                UsuarioId,
                Nombre,
                Fecha,
                Activo
            FROM MenusDiarios";

        private readonly SqlConnectionFactory connectionFactory;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="MenuDiarioRepository"/>.
        /// </summary>
        /// <param name="connectionFactory">Fábrica de conexiones SQL.</param>
        public MenuDiarioRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Obtiene todos los menús diarios registrados.
        /// </summary>
        /// <returns>Lista de menús diarios.</returns>
        public List<MenuDiario> GetAll()
        {
            var menus = new List<MenuDiario>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            using var command = new SqlCommand(SelectMenusQuery + ";", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                menus.Add(MapMenu(reader));
            }

            return menus;
        }

        /// <summary>
        /// Obtiene un menú diario por su identificador.
        /// </summary>
        /// <param name="menuId">Identificador del menú.</param>
        /// <returns>Menú encontrado o null.</returns>
        public MenuDiario? GetById(Guid menuId)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = SelectMenusQuery + @"
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", menuId);

            using var reader = command.ExecuteReader();

            return reader.Read() ? MapMenu(reader) : null;
        }

        /// <summary>
        /// Crea un nuevo menú diario en la base de datos.
        /// </summary>
        /// <param name="menu">Menú a registrar.</param>
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

        /// <summary>
        /// Actualiza un menú diario existente en la base de datos.
        /// </summary>
        /// <param name="menu">Menú a actualizar.</param>
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

        /// <summary>
        /// Elimina un menú diario por su identificador.
        /// </summary>
        /// <param name="menuId">Identificador del menú.</param>
        public void Delete(Guid menuId)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"DELETE FROM MenusDiarios WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", menuId);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Obtiene un menú diario asociado a un usuario en una fecha específica.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <param name="fecha">Fecha del menú.</param>
        /// <returns>Menú encontrado o null.</returns>
        public MenuDiario? GetByUsuarioYFecha(Guid usuarioId, DateTime fecha)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = SelectMenusQuery + @"
                WHERE UsuarioId = @UsuarioId
                  AND Fecha = @Fecha;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UsuarioId", usuarioId);
            command.Parameters.AddWithValue("@Fecha", fecha.Date);

            using var reader = command.ExecuteReader();

            return reader.Read() ? MapMenu(reader) : null;
        }

        /// <summary>
        /// Agrega al comando SQL los parámetros comunes de menú diario.
        /// </summary>
        /// <param name="command">Comando SQL a completar.</param>
        /// <param name="menu">Menú fuente de datos.</param>
        private static void AddCommonParameters(SqlCommand command, MenuDiario menu)
        {
            command.Parameters.AddWithValue("@Id", menu.Id);
            command.Parameters.AddWithValue("@UsuarioId", menu.UsuarioId);
            command.Parameters.AddWithValue("@Nombre", menu.Nombre);
            command.Parameters.AddWithValue("@Fecha", menu.Fecha.Date);
            command.Parameters.AddWithValue("@Activo", menu.Activo);
        }

        /// <summary>
        /// Mapea un registro de base de datos a una instancia de <see cref="MenuDiario"/>.
        /// </summary>
        /// <param name="reader">Lector de datos SQL posicionado sobre un registro válido.</param>
        /// <returns>Instancia mapeada de menú diario.</returns>
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