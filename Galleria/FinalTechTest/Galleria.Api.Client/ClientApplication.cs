using Galleria.Api.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Galleria.Api.Client
{
    /// <summary>
    /// A class that handles the logic of the client application.
    /// </summary>
    public sealed class ClientApplication : IDisposable
    {
        private readonly UserProfileService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientApplication"/> class.
        /// </summary>
        /// <param name="serviceAddress">The address at which the service is hosted.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="serviceAddress"/> is null or empty.</exception>
        public ClientApplication(string serviceAddress)
        {
            Verify.NotNullOrEmpty(serviceAddress, nameof(serviceAddress));

            _service = new UserProfileService(serviceAddress);
        }

        /// <summary>
        /// Runs the client application.
        /// </summary>
        public void Run()
        {
            GetAllUsers();
            GetUsersForCompany2();
        }

        private void GetAllUsers()
        {
            Console.WriteLine("Getting all users...");

            var users = _service.GetUsers();
            DisplayUsers(users);
        }

        private void GetUsersForCompany2()
        {
            Console.WriteLine();
            Console.WriteLine("Getting users for company 2...");

            var users = _service.GetUsers(companyId: 2);
            DisplayUsers(users);
        }

        private static void DisplayUsers(IEnumerable<UserProfile> users)
        {
            if (!users.Any())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No users found");
                Console.ResetColor();
            }

            foreach (var user in users)
            {
                DisplayUser(user);
            }
        }

        private static void DisplayUser(UserProfile user)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            if (user == null)
            {
                Console.WriteLine("No user");
            }
            else
            {
                Console.WriteLine($"User {user.UserId} for Company {user.CompanyId}: {user.Title} {user.Forename} {user.Surname} born on {user.DateOfBirth:dd MMM yyyy}");
            }

            Console.ResetColor();
        }

        public void Dispose()
        {
            _service.Dispose();
        }
    }
}