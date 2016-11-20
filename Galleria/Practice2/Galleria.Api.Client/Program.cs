using Galleria.Api.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace Galleria.Api.Client
{
    public sealed class Program : IDisposable
    {
        private readonly UserProfileApiClient _client;

        public Program(string serviceAddress)
        {
            _client = new UserProfileApiClient(serviceAddress);
        }

        public void Run()
        {
            GetAllUsers();
            GetUsersForCompany2();
            DeleteUser1();
            CreateUser4();
            GetUser4();
            UpdateUser4();
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

        private void DeleteUser1()
        {
            _client.DeleteUser(userId: 1);
        }

        private void CreateUser4()
        {
            var user = new UserProfile()
            {
                CompanyId = 1,
                Title = "Mr",
                Forename = "User",
                Surname = "Four",
                DateOfBirth = DateTime.Today
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
                CompanyId = 1,
                Title = "Dr",
                Forename = "UpdatedUser",
                Surname = "UpdatedFour",
                DateOfBirth = DateTime.Today.AddYears(-20)
            };

            _client.UpdateUser(user);
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
                Console.WriteLine($"User {user.UserId} for Company {user.CompanyId}: {user.Title} {user.Forename} {user.Surname} born on {user.DateOfBirth:dd MMM yyyy}");
            }

            Console.ResetColor();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public static void Main(string[] args)
        {
            try
            {
                string serviceAddress = ConfigurationManager.AppSettings["ServiceAddress"];
                using (var program = new Program(serviceAddress))
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

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(intercept: true);
            }
        }
    }
}