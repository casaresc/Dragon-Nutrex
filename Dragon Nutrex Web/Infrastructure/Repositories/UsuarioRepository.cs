using Dragon_Nutrex_Web.Core.Enums;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio SQL Server para la persistencia de usuarios.
    /// </summary>
    public class UsuarioRepository : IRepository<Usuario>
    {
        private const string SelectUsuariosQuery = @"
            SELECT
                Id,
                Nombre,
                Correo,
                Contrasena,
                Rol,
                Peso,
                Altura,
                Edad,
                NivelActividad,
                Objetivo,
                TipoDieta,
                Activo
            FROM Usuarios";

        private readonly SqlConnectionFactory connectionFactory;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="UsuarioRepository"/>.
        /// </summary>
        /// <param name="connectionFactory">Fábrica de conexiones SQL.</param>
        public UsuarioRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        public List<Usuario> GetAll()
        {
            var usuarios = new List<Usuario>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            using var command = new SqlCommand(SelectUsuariosQuery + ";", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                usuarios.Add(MapUsuario(reader));
            }

            return usuarios;
        }

        /// <summary>
        /// Obtiene un usuario por su identificador.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <returns>Usuario encontrado o null.</returns>
        public Usuario? GetById(Guid usuarioId)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = SelectUsuariosQuery + @"
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", usuarioId);

            using var reader = command.ExecuteReader();

            return reader.Read() ? MapUsuario(reader) : null;
        }

        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Usuario a registrar.</param>
        public void Create(Usuario usuario)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                INSERT INTO Usuarios (
                    Id,
                    Nombre,
                    Correo,
                    Contrasena,
                    Rol,
                    Peso,
                    Altura,
                    Edad,
                    NivelActividad,
                    Objetivo,
                    TipoDieta,
                    Activo,
                    FechaCreacion
                )
                VALUES (
                    @Id,
                    @Nombre,
                    @Correo,
                    @Contrasena,
                    @Rol,
                    @Peso,
                    @Altura,
                    @Edad,
                    @NivelActividad,
                    @Objetivo,
                    @TipoDieta,
                    @Activo,
                    SYSDATETIME()
                );";

            using var command = new SqlCommand(query, connection);
            AddCommonParameters(command, usuario);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Actualiza un usuario existente en la base de datos.
        /// </summary>
        /// <param name="usuario">Usuario a actualizar.</param>
        public void Update(Usuario usuario)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                UPDATE Usuarios
                SET
                    Nombre = @Nombre,
                    Correo = @Correo,
                    Contrasena = @Contrasena,
                    Rol = @Rol,
                    Peso = @Peso,
                    Altura = @Altura,
                    Edad = @Edad,
                    NivelActividad = @NivelActividad,
                    Objetivo = @Objetivo,
                    TipoDieta = @TipoDieta,
                    Activo = @Activo,
                    FechaActualizacion = SYSDATETIME()
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            AddCommonParameters(command, usuario);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Elimina un usuario por su identificador.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        public void Delete(Guid usuarioId)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"DELETE FROM Usuarios WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", usuarioId);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Agrega al comando SQL los parámetros comunes de usuario.
        /// </summary>
        /// <param name="command">Comando SQL a completar.</param>
        /// <param name="usuario">Usuario fuente de datos.</param>
        private static void AddCommonParameters(SqlCommand command, Usuario usuario)
        {
            command.Parameters.AddWithValue("@Id", usuario.Id);
            command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            command.Parameters.AddWithValue("@Correo", usuario.Correo);
            command.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
            command.Parameters.AddWithValue("@Rol", usuario.Rol);
            command.Parameters.AddWithValue("@Peso", usuario.Peso);
            command.Parameters.AddWithValue("@Altura", usuario.Altura);
            command.Parameters.AddWithValue("@Edad", usuario.Edad);
            command.Parameters.AddWithValue("@NivelActividad", usuario.NivelActividad.ToString());
            command.Parameters.AddWithValue("@Objetivo", usuario.Objetivo.ToString());
            command.Parameters.AddWithValue("@TipoDieta", usuario.TipoDieta.ToString());
            command.Parameters.AddWithValue("@Activo", usuario.Activo);
        }

        /// <summary>
        /// Mapea un registro de base de datos a una instancia de <see cref="Usuario"/>.
        /// </summary>
        /// <param name="reader">Lector de datos SQL posicionado sobre un registro válido.</param>
        /// <returns>Instancia mapeada de usuario.</returns>
        private static Usuario MapUsuario(SqlDataReader reader)
        {
            return new Usuario
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Correo = reader["Correo"].ToString() ?? string.Empty,
                Contrasena = reader["Contrasena"].ToString() ?? string.Empty,
                Rol = reader["Rol"].ToString() ?? "Usuario",
                Peso = Convert.ToDecimal(reader["Peso"]),
                Altura = Convert.ToDecimal(reader["Altura"]),
                Edad = Convert.ToInt32(reader["Edad"]),
                NivelActividad = Enum.Parse<NivelActividad>(reader["NivelActividad"].ToString() ?? "Sedentario"),
                Objetivo = Enum.Parse<ObjetivoNutricional>(reader["Objetivo"].ToString() ?? "MantenerPeso"),
                TipoDieta = Enum.Parse<TipoDieta>(reader["TipoDieta"].ToString() ?? "Balanceada"),
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }
    }
}