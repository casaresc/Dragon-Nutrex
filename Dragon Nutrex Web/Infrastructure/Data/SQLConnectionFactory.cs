using Microsoft.Data.SqlClient;

namespace Dragon_Nutrex_Web.Infrastructure.Data
{
    /// <summary>
    /// Fabrica conexiones SQL para la aplicación.
    /// </summary>
    public class SqlConnectionFactory
    {
        private readonly string connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Crea una nueva conexión SQL.
        /// </summary>
        /// <returns>Conexión SQL Server.</returns>
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}