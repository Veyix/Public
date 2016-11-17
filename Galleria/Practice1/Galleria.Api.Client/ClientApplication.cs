using Galleria.Api.Contract;
using System;
using System.Collections.Generic;

namespace Galleria.Api.Client
{
    public sealed class ClientApplication : IDisposable
    {
        private readonly UserProfileApiClient _client;

        public ClientApplication(string address)
        {
            Verify.NotNullOrEmpty(address, nameof(address));

            _client = new UserProfileApiClient(address);
        }

        public void Run()
        {
            CreateUser1();
            CreateUser2();
            CreateUser3();
            ShowAllUsers();
            ShowUsersForCompany2();
            UpdateUser2();
            DeleteUser3();
            ShowAllUsers();
        }

        private void CreateUser1()
        {
            var user = new UserProfile()
            {
                CompanyId = 1,
                Title = "Mr",
                Forename = "Samuel",
                Surname = "Slade",
                DateOfBirth = new DateTime(1988, 9, 13)
            };

            _client.CreateUser(user);
        }

        private void CreateUser2()
        {
            var user = new UserProfile()
            {
                CompanyId = 1,
                Title = "Miss",
                Forename = "Jane",
                Surname = "George",
                DateOfBirth = new DateTime(1990, 1, 4)
            };

            _client.CreateUser(user);
        }

        private void CreateUser3()
        {
            var user = new UserProfile()
            {
                CompanyId = 2,
                Title = "Dr",
                Forename = "Fred",
                Surname = "Dave",
                DateOfBirth = new DateTime(1975, 1, 6)
            };

            _client.CreateUser(user);
        }

        private void ShowAllUsers()
        {
            var users = _client.GetUsers();
            DisplayUsers(users);
        }

        private void ShowUsersForCompany2()
        {
            var users = _client.GetUsers(companyId: 2);
            DisplayUsers(users);
        }

        private void UpdateUser2()
        {
            var user = new UserProfile()
            {
                CompanyId = 1,
                Title = "Mrs",
                Forename = "Jane",
                Surname = "Bobston",
                DateOfBirth = new DateTime(1990, 1, 4)
            };

            _client.UpdateUser(user);
        }

        private void ShowUser2()
        {
            var user = _client.GetUser(userId: 2);
            DisplayUser(user);
        }

        private void DeleteUser3()
        {
            _client.DeleteUser(userId: 3);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        private static void DisplayUsers(IEnumerable<UserProfile> users)
        {
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
                Console.WriteLine("No user...");
            }
            else
            {
                Console.WriteLine($"User {user.UserId} for Company {user.CompanyId}: {user.Title} {user.Forename} {user.Surname}, born on {user.DateOfBirth:dd MMM yyyy}.");
            }

            Console.ResetColor();
        }
    }
}