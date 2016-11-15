using System.Data.SqlClient;

namespace Galleria.Profiles.Infrastructure.AdoNet
{
    /// <summary>
    /// Provides a contract for a factory that creates SQL connections.
    /// </summary>
    public interface ISqlConnectionFactory
    {
        /// <summary>
        /// Creates a new SQL connection.
        /// </summary>
        /// <returns>A new instance of the <see cref="SqlConnection"/> class.</returns>
        SqlConnection CreateConnection();
    }
}