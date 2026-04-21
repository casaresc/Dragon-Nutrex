using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio SQL Server para la persistencia de consumos diarios.
    /// </summary>
    public class ConsumoDiarioRepository : IConsumoDiarioRepository
    {
        private const string SelectConsumosQuery = @"
            SELECT
                Id,
                UsuarioId,
                Fecha,
                CaloriasConsumidas,
                CarbohidratosConsumidos,
                ProteinasConsumidas,
                GrasasConsumidas,
                Activo
            FROM ConsumosDiarios";

        private readonly SqlConnectionFactory connectionFactory;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ConsumoDiarioRepository"/>.
        /// </summary>
        /// <param name="connectionFactory">Fábrica de conexiones SQL.</param>
        public ConsumoDiarioRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Obtiene todos los consumos diarios registrados.
        /// </summary>
        /// <returns>Lista de consumos diarios.</returns>
        public List<ConsumoDiario> GetAll()
        {
            var consumos = new List<ConsumoDiario>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            using var command = new SqlCommand(SelectConsumosQuery + ";", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                consumos.Add(MapConsumo(reader));
            }

            return consumos;
        }

        /// <summary>
        /// Obtiene un consumo diario por su identificador.
        /// </summary>
        /// <param name="consumoId">Identificador del consumo.</param>
        /// <returns>Consumo encontrado o null.</returns>
        public ConsumoDiario? GetById(Guid consumoId)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = SelectConsumosQuery + @"
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", consumoId);

            using var reader = command.ExecuteReader();

            return reader.Read() ? MapConsumo(reader) : null;
        }

        /// <summary>
        /// Crea un nuevo consumo diario en la base de datos.
        /// </summary>
        /// <param name="consumo">Consumo a registrar.</param>
        public void Create(ConsumoDiario consumo)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                INSERT INTO ConsumosDiarios (
                    Id,
                    UsuarioId,
                    Fecha,
                    CaloriasConsumidas,
                    CarbohidratosConsumidos,
                    ProteinasConsumidas,
                    GrasasConsumidas,
                    Activo,
                    FechaCreacion
                )
                VALUES (
                    @Id,
                    @UsuarioId,
                    @Fecha,
                    @CaloriasConsumidas,
                    @CarbohidratosConsumidos,
                    @ProteinasConsumidas,
                    @GrasasConsumidas,
                    @Activo,
                    SYSDATETIME()
                );";

            using var command = new SqlCommand(query, connection);
            AddCommonParameters(command, consumo);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Actualiza un consumo diario existente en la base de datos.
        /// </summary>
        /// <param name="consumo">Consumo a actualizar.</param>
        public void Update(ConsumoDiario consumo)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                UPDATE ConsumosDiarios
                SET
                    UsuarioId = @UsuarioId,
                    Fecha = @Fecha,
                    CaloriasConsumidas = @CaloriasConsumidas,
                    CarbohidratosConsumidos = @CarbohidratosConsumidos,
                    ProteinasConsumidas = @ProteinasConsumidas,
                    GrasasConsumidas = @GrasasConsumidas,
                    Activo = @Activo
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            AddCommonParameters(command, consumo);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Elimina un consumo diario por su identificador.
        /// </summary>
        /// <param name="consumoId">Identificador del consumo.</param>
        public void Delete(Guid consumoId)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"DELETE FROM ConsumosDiarios WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", consumoId);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Obtiene los consumos registrados en una fecha específica.
        /// </summary>
        /// <param name="fecha">Fecha a consultar.</param>
        /// <returns>Lista de consumos de la fecha.</returns>
        public List<ConsumoDiario> GetByDate(DateTime fecha)
        {
            var consumos = new List<ConsumoDiario>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = SelectConsumosQuery + @"
                WHERE Fecha = @Fecha;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Fecha", fecha.Date);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                consumos.Add(MapConsumo(reader));
            }

            return consumos;
        }

        /// <summary>
        /// Obtiene los consumos registrados dentro de un rango de fechas.
        /// </summary>
        /// <param name="fechaInicio">Fecha inicial del rango.</param>
        /// <param name="fechaFin">Fecha final del rango.</param>
        /// <returns>Lista de consumos dentro del rango.</returns>
        public List<ConsumoDiario> GetByRange(DateTime fechaInicio, DateTime fechaFin)
        {
            var consumos = new List<ConsumoDiario>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = SelectConsumosQuery + @"
                WHERE Fecha >= @FechaInicio
                  AND Fecha <= @FechaFin;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);
            command.Parameters.AddWithValue("@FechaFin", fechaFin.Date);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                consumos.Add(MapConsumo(reader));
            }

            return consumos;
        }

        /// <summary>
        /// Agrega al comando SQL los parámetros comunes de consumo diario.
        /// </summary>
        /// <param name="command">Comando SQL a completar.</param>
        /// <param name="consumo">Consumo fuente de datos.</param>
        private static void AddCommonParameters(SqlCommand command, ConsumoDiario consumo)
        {
            command.Parameters.AddWithValue("@Id", consumo.Id);
            command.Parameters.AddWithValue("@UsuarioId", consumo.UsuarioId);
            command.Parameters.AddWithValue("@Fecha", consumo.Fecha.Date);
            command.Parameters.AddWithValue("@CaloriasConsumidas", consumo.CaloriasConsumidas);
            command.Parameters.AddWithValue("@CarbohidratosConsumidos", consumo.CarbohidratosConsumidos);
            command.Parameters.AddWithValue("@ProteinasConsumidas", consumo.ProteinasConsumidas);
            command.Parameters.AddWithValue("@GrasasConsumidas", consumo.GrasasConsumidas);
            command.Parameters.AddWithValue("@Activo", consumo.Activo);
        }

        /// <summary>
        /// Mapea un registro de base de datos a una instancia de <see cref="ConsumoDiario"/>.
        /// </summary>
        /// <param name="reader">Lector de datos SQL posicionado sobre un registro válido.</param>
        /// <returns>Instancia mapeada de consumo diario.</returns>
        private static ConsumoDiario MapConsumo(SqlDataReader reader)
        {
            return new ConsumoDiario
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                UsuarioId = reader.GetGuid(reader.GetOrdinal("UsuarioId")),
                Fecha = Convert.ToDateTime(reader["Fecha"]),
                CaloriasConsumidas = Convert.ToDecimal(reader["CaloriasConsumidas"]),
                CarbohidratosConsumidos = Convert.ToDecimal(reader["CarbohidratosConsumidos"]),
                ProteinasConsumidas = Convert.ToDecimal(reader["ProteinasConsumidas"]),
                GrasasConsumidas = Convert.ToDecimal(reader["GrasasConsumidas"]),
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }
    }
}