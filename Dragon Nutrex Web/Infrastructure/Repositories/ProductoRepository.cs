using Dragon_Nutrex_Web.Core.Enums;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio SQL Server para productos.
    /// </summary>
    public class ProductoRepository : IRepository<Producto>
    {
        private readonly SqlConnectionFactory connectionFactory;

        public ProductoRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public List<Producto> GetAll()
        {
            var productos = new List<Producto>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    Nombre,
                    Categoria,
                    Proteina,
                    Carbohidratos,
                    Grasas,
                    PorcionGramos,
                    Calorias,
                    Activo
                FROM Productos;";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                productos.Add(MapProducto(reader));
            }

            return productos;
        }

        public Producto? GetById(Guid id)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    Id,
                    Nombre,
                    Categoria,
                    Proteina,
                    Carbohidratos,
                    Grasas,
                    PorcionGramos,
                    Calorias,
                    Activo
                FROM Productos
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapProducto(reader);
            }

            return null;
        }

        public void Create(Producto producto)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                INSERT INTO Productos (
                    Id,
                    Nombre,
                    Categoria,
                    Proteina,
                    Carbohidratos,
                    Grasas,
                    PorcionGramos,
                    Calorias,
                    Activo,
                    FechaCreacion
                )
                VALUES (
                    @Id,
                    @Nombre,
                    @Categoria,
                    @Proteina,
                    @Carbohidratos,
                    @Grasas,
                    @PorcionGramos,
                    @Calorias,
                    @Activo,
                    SYSDATETIME()
                );";

            using var command = new SqlCommand(query, connection);
            AddCommonParameters(command, producto);

            command.ExecuteNonQuery();
        }

        public void Update(Producto producto)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                UPDATE Productos
                SET
                    Nombre = @Nombre,
                    Categoria = @Categoria,
                    Proteina = @Proteina,
                    Carbohidratos = @Carbohidratos,
                    Grasas = @Grasas,
                    PorcionGramos = @PorcionGramos,
                    Calorias = @Calorias,
                    Activo = @Activo,
                    FechaActualizacion = SYSDATETIME()
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            AddCommonParameters(command, producto);

            command.ExecuteNonQuery();
        }

        public void Delete(Guid id)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"DELETE FROM Productos WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        private static void AddCommonParameters(SqlCommand command, Producto producto)
        {
            command.Parameters.AddWithValue("@Id", producto.Id);
            command.Parameters.AddWithValue("@Nombre", producto.Nombre);
            command.Parameters.AddWithValue("@Categoria", producto.Categoria.ToString());
            command.Parameters.AddWithValue("@Proteina", producto.Proteina);
            command.Parameters.AddWithValue("@Carbohidratos", producto.Carbohidratos);
            command.Parameters.AddWithValue("@Grasas", producto.Grasas);
            command.Parameters.AddWithValue("@PorcionGramos", producto.PorcionGramos);
            command.Parameters.AddWithValue("@Calorias", producto.Calorias);
            command.Parameters.AddWithValue("@Activo", producto.Activo);
        }

        private static Producto MapProducto(SqlDataReader reader)
        {
            return new Producto
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Categoria = Enum.Parse<CategoriaProducto>(reader["Categoria"].ToString() ?? "Otro"),
                Proteina = Convert.ToDecimal(reader["Proteina"]),
                Carbohidratos = Convert.ToDecimal(reader["Carbohidratos"]),
                Grasas = Convert.ToDecimal(reader["Grasas"]),
                PorcionGramos = Convert.ToDecimal(reader["PorcionGramos"]),
                Calorias = Convert.ToDecimal(reader["Calorias"]),
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }
    }
}