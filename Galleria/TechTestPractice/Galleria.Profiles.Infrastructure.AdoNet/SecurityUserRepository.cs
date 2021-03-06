﻿using Galleria.Profiles.ObjectModel;
using Galleria.Support;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Galleria.Profiles.Infrastructure.AdoNet
{
    /// <summary>
    /// A class that manages instances of the <see cref="SecurityUser"/> class.
    /// </summary>
    public sealed class SecurityUserRepository : ISecurityUserRepository
    {
        private const string COMMAND_GET_BY_USERNAME = "dbo.SecurityUserGetByUsername";

        private readonly SqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityUserRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">A factory that creates database connections.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="connectionFactory"/> is null.</exception>
        public SecurityUserRepository(ISqlConnectionFactory connectionFactory)
        {
            Verify.NotNull(connectionFactory, nameof(connectionFactory));

            _connection = connectionFactory.CreateConnection();
        }

        public SecurityUser GetByUsername(string username)
        {
            Verify.NotNullOrEmpty(username, nameof(username));

            EnsureConnectionOpen();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = COMMAND_GET_BY_USERNAME;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@strUsername", username).SqlDbType = SqlDbType.VarChar;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return CreateSecurityUser(reader);
                    }
                }
            }

            // We didn't find the security user
            return null;
        }

        private void EnsureConnectionOpen()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        private static SecurityUser CreateSecurityUser(IDataReader reader)
        {
            var securityUser = new SecurityUser();

            securityUser.UserId = Convert.ToInt32(reader["UserId"]);
            securityUser.Username = Convert.ToString(reader["Username"]);
            securityUser.Password = Convert.ToString(reader["Password"]);

            return securityUser;
        }
    }
}