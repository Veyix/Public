using Galleria.Profiles.ObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Galleria.Profiles.Infrastructure.AdoNet
{
    /// <summary>
    /// A class that manages instances of the <see cref="UserProfile"/> class.
    /// </summary>
    public sealed class UserProfileRepository : IUserProfileRepository
    {
        private const string COMMAND_GET_USER_PROFILE = "dbo.UserProfileGetById";
        private const string COMMAND_GET_USER_PROFILES = "dbo.UserProfileGetAll";
        private const string COMMAND_GET_USER_PROFILES_COMPANY = "dbo.UserProfileGetByCompanyId";

        private readonly SqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileRepository"/> class.
        /// </summary>
        /// <param name="connection">A connection to the database.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="connection"/> is null.</exception>
        public UserProfileRepository(SqlConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            _connection = connection;
        }

        public UserProfile GetUserProfile(int userId)
        {
            EnsureConnectionOpen();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = COMMAND_GET_USER_PROFILE;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@intUserId", userId).SqlDbType = SqlDbType.Int;

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return CreateUserProfile(reader);
                }
            }

            // If we got here, we didn't find the record
            return null;
        }

        public IReadOnlyCollection<UserProfile> GetUserProfiles()
        {
            EnsureConnectionOpen();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = COMMAND_GET_USER_PROFILES;
                command.CommandType = CommandType.StoredProcedure;

                var reader = command.ExecuteReader();
                return CreateUserProfiles(reader).ToArray();
            }
        }

        public IReadOnlyCollection<UserProfile> GetUserProfiles(int companyId)
        {
            EnsureConnectionOpen();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = COMMAND_GET_USER_PROFILES_COMPANY;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@intCompanyId", companyId).SqlDbType = SqlDbType.Int;

                var reader = command.ExecuteReader();
                return CreateUserProfiles(reader).ToArray();
            }
        }

        private void EnsureConnectionOpen()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        private static IEnumerable<UserProfile> CreateUserProfiles(IDataReader reader)
        {
            while (reader.Read())
            {
                yield return CreateUserProfile(reader);
            }
        }

        private static UserProfile CreateUserProfile(IDataReader reader)
        {
            int userId = (int)reader["Id"];
            int companyId = (int)reader["CompanyId"];
            string title = (string)reader["Title"];
            string forename = (string)reader["Forename"];
            string surname = (string)reader["Surname"];
            var dateOfBirth = (DateTime)reader["DateOfBirth"];
            var createdDate = (DateTime)reader["CreatedDate"];
            var lastChangedDate = (DateTime)reader["LastChangedDate"];

            return new UserProfile()
            {
                Id = userId,
                CompanyId = companyId,
                Title = title,
                Forename = forename,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                CreatedDate = createdDate,
                LastChangedDate = lastChangedDate
            };
        }
    }
}