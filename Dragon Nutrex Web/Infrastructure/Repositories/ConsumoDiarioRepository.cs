using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio SQL Server para consumos diarios.
    /// </summary>
    public class ConsumoDiarioRepository : IRepository<ConsumoDiario>
    {
        private readonly SqlConnectionFactory connectionFactory;

        public ConsumoDiarioRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public List<ConsumoDiario> GetAll()
        {
            var consumos = new List<ConsumoDiario>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    UsuarioId,
                    Fecha,
                    CaloriasConsumidas,
                    CarbohidratosConsumidos,
                    ProteinasConsumidas,
                    GrasasConsumidas,
                    Activo
                FROM ConsumosDiarios;";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                consumos.Add(MapConsumo(reader));
            }

            return consumos;
        }

        public ConsumoDiario? GetById(Guid id)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    UsuarioId,
                    Fecha,
                    CaloriasConsumidas,
                    CarbohidratosConsumidos,
                    ProteinasConsumidas,
                    GrasasConsumidas,
                    Activo
                FROM ConsumosDiarios
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapConsumo(reader);
            }

            return null;
        }

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

        public void Delete(Guid id)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"DELETE FROM ConsumosDiarios WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        public List<ConsumoDiario> GetByDate(DateTime fecha)
        {
            var consumos = new List<ConsumoDiario>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    UsuarioId,
                    Fecha,
                    CaloriasConsumidas,
                    CarbohidratosConsumidos,
                    ProteinasConsumidas,
                    GrasasConsumidas,
                    Activo
                FROM ConsumosDiarios
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

        public List<ConsumoDiario> GetByRange(DateTime fechaInicio, DateTime fechaFin)
        {
            var consumos = new List<ConsumoDiario>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    UsuarioId,
                    Fecha,
                    CaloriasConsumidas,
                    CarbohidratosConsumidos,
                    ProteinasConsumidas,
                    GrasasConsumidas,
                    Activo
                FROM ConsumosDiarios
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