using System.Configuration;
using System.Data.SqlClient;

namespace Galleria.Profiles.Api.Service
{
    /// <summary>
    /// A class that handles the creation of connections to the database.
    /// </summary>
    public static class ConnectionFactory
    {
        public static SqlConnection CreateConnection()
        {
            string connectionString = GetConnectionString();
            return new SqlConnection(connectionString);
        }

        private static string GetConnectionString()
        {
            var settings = ConfigurationManager.ConnectionStrings["Galleria"];
            return settings?.ConnectionString;
        }
    }
}