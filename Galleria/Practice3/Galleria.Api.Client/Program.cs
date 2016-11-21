using Galleria.Api.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Galleria.Api.Client
{
    public sealed class Program : IDisposable
    {
        private readonly UserProfileApiClient _client;

        public Program(UserProfileApiClient client)
        {
            _client = client;
        }

        private void Run()
        {
            GetAllUsers();
            GetUsersForCompany2();
            CreateUser4();
            GetUser4();
            UpdateUser4();
            DeleteUser3();
            GetAllUsers();
        }

        private void GetAllUsers()
        {
            var users = _client.GetUsers();
            DisplayUsers(users);
        }

        private void GetUsersForCompany2()
        {
            var users = _client.GetUsers(companyId: 2);
            DisplayUsers(users);
        }

        private void CreateUser4()
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

        private void GetUser4()
        {
            var user = _client.GetUser(userId: 4);
            DisplayUser(user);
        }

        private void UpdateUser4()
        {
            var user = new UserProfile()
            {
                Id = 4,
                CompanyId = 2,
                Title = "Dr",
                Forename = "Sam",
                Surname = "Slade",
                DateOfBirth = new DateTime(1988, 9, 13)
            };

            _client.UpdateUser(user);
        }

        private void DeleteUser3()
        {
            _client.DeleteUser(userId: 3);
        }

        private void DisplayUsers(IEnumerable<UserProfile> users)
        {
            foreach (var user in users)
            {
                DisplayUser(user);
            }
        }

        private void DisplayUser(UserProfile user)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            if (user == null)
            {
                Console.WriteLine("No user...");
            }
            else
            {
                Console.WriteLine($"User {user.Id} for Company {user.CompanyId}: {user.Title} {user.Forename} {user.Surname} born on {user.DateOfBirth:dd MMM yyyy}");
            }

            Console.ResetColor();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public static void Main()
        {
            string serviceAddress = ConfigurationManager.AppSettings["ServiceAddress"];
            var client = new UserProfileApiClient(serviceAddress);

            try
            {
                using (var program = new Program(client))
                {
                    program.Run();
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Console.ResetColor();
            }
        }
    }
}