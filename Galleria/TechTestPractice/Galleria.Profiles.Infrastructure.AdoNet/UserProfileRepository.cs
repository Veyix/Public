using Galleria.Profiles.ObjectModel;
using Galleria.Support;
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
        private const string COMMAND_ADD_USER_PROFILE = "dbo.UserProfileCreate";
        private const string COMMAND_UPDATE_USER_PROFILE = "dbo.UserProfileUpdate";
        private const string COMMAND_DELETE_USER_PROFILE = "dbo.UserProfileDelete";

        private readonly SqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">A factory that creates database connections.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="connectionFactory"/> is null.</exception>
        public UserProfileRepository(ISqlConnectionFactory connectionFactory)
        {
            Verify.NotNull(connectionFactory, nameof(connectionFactory));

            _connection = connectionFactory.CreateConnection();
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

        /// <summary>
        /// Adds or updates the given user profile record in the database.
        /// </summary>
        /// <param name="profile">The user profile record to be saved.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is null.</exception>
        public void SaveUserProfile(UserProfile profile)
        {
            Verify.NotNull(profile, nameof(profile));

            EnsureConnectionOpen();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = COMMAND_ADD_USER_PROFILE;
                command.CommandType = CommandType.StoredProcedure;

                if (profile.Id != 0)
                {
                    // This is not a new profile, so adjust the command to update the existing record
                    command.CommandText = COMMAND_UPDATE_USER_PROFILE;
                    command.Parameters.AddWithValue("@intUserId", profile.Id).SqlDbType = SqlDbType.Int;
                }

                command.Parameters.AddWithValue("@intCompanyId", profile.CompanyId).SqlDbType = SqlDbType.Int;
                command.Parameters.AddWithValue("@strTitle", profile.Title).SqlDbType = SqlDbType.VarChar;
                command.Parameters.AddWithValue("@strForename", profile.Forename).SqlDbType = SqlDbType.VarChar;
                command.Parameters.AddWithValue("@strSurname", profile.Surname).SqlDbType = SqlDbType.VarChar;
                command.Parameters.AddWithValue("@dteDateOfBirth", profile.DateOfBirth).SqlDbType = SqlDbType.Date;

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes the specified user profile.
        /// </summary>
        /// <param name="userId">The Id of the user profile to be deleted.</param>
        public void DeleteUserProfile(int userId)
        {
            EnsureConnectionOpen();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = COMMAND_DELETE_USER_PROFILE;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@intUserId", userId).SqlDbType = SqlDbType.Int;

                command.ExecuteNonQuery();
            }
        }

        private void EnsureConnectionOpen()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }
    }
}