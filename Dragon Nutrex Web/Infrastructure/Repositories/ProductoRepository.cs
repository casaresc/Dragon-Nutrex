using Dragon_Nutrex_Web.Core.Enums;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio SQL Server para la persistencia de productos.
    /// </summary>
    public class ProductoRepository : IRepository<Producto>
    {
        private const string SelectProductosQuery = @"
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
            FROM Productos";

        private readonly SqlConnectionFactory connectionFactory;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProductoRepository"/>.
        /// </summary>
        /// <param name="connectionFactory">Fábrica de conexiones SQL.</param>
        public ProductoRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Obtiene todos los productos registrados.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        public List<Producto> GetAll()
        {
            var productos = new List<Producto>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            using var command = new SqlCommand(SelectProductosQuery + ";", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                productos.Add(MapProducto(reader));
            }

            return productos;
        }

        /// <summary>
        /// Obtiene un producto por su identificador.
        /// </summary>
        /// <param name="productoId">Identificador del producto.</param>
        /// <returns>Producto encontrado o null.</returns>
        public Producto? GetById(Guid productoId)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = SelectProductosQuery + @"
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", productoId);

            using var reader = command.ExecuteReader();

            return reader.Read() ? MapProducto(reader) : null;
        }

        /// <summary>
        /// Crea un nuevo producto en la base de datos.
        /// </summary>
        /// <param name="producto">Producto a registrar.</param>
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

        /// <summary>
        /// Actualiza un producto existente en la base de datos.
        /// </summary>
        /// <param name="producto">Producto a actualizar.</param>
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

        /// <summary>
        /// Elimina un producto por su identificador.
        /// </summary>
        /// <param name="productoId">Identificador del producto.</param>
        public void Delete(Guid productoId)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"DELETE FROM Productos WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", productoId);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Agrega al comando SQL los parámetros comunes de producto.
        /// </summary>
        /// <param name="command">Comando SQL a completar.</param>
        /// <param name="producto">Producto fuente de datos.</param>
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

        /// <summary>
        /// Mapea un registro de base de datos a una instancia de <see cref="Producto"/>.
        /// </summary>
        /// <param name="reader">Lector de datos SQL posicionado sobre un registro válido.</param>
        /// <returns>Instancia mapeada de producto.</returns>
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