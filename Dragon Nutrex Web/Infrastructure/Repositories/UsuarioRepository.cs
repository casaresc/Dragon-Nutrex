using Dragon_Nutrex_Web.Core.Enums;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio SQL Server para usuarios.
    /// </summary>
    public class UsuarioRepository : IRepository<Usuario>
    {
        private readonly SqlConnectionFactory connectionFactory;

        public UsuarioRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public List<Usuario> GetAll()
        {
            var usuarios = new List<Usuario>();

            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
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
                FROM Usuarios;";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                usuarios.Add(MapUsuario(reader));
            }

            return usuarios;
        }

        public Usuario? GetById(Guid id)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
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
                FROM Usuarios
                WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapUsuario(reader);
            }

            return null;
        }

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

        public void Delete(Guid id)
        {
            using var connection = connectionFactory.CreateConnection();
            connection.Open();

            const string query = @"DELETE FROM Usuarios WHERE Id = @Id;";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

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