using System;
using System.Configuration;
using System.Net.Http;

namespace Galleria.Profiles.Api.Client
{
    /// <summary>
    /// A class that provides the main entry point for the application.
    /// </summary>
    public sealed class Program
    {
        private readonly IInputReceiver _inputReceiver;
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        /// <param name="inputReceiver">A receiver of input commands.</param>
        /// <param name="apiAddress">The URL to the hosted API.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="inputReceiver"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="apiAddress"/> is null or empty.</exception>
        public Program(IInputReceiver inputReceiver, string apiAddress)
        {
            if (inputReceiver == null) throw new ArgumentNullException(nameof(inputReceiver));
            if (String.IsNullOrWhiteSpace(apiAddress)) throw new ArgumentException("The API address cannot be empty", nameof(apiAddress));

            _inputReceiver = inputReceiver;

            // Setup and initialize the API client
            _client = new HttpClient();
            _client.BaseAddress = new Uri(apiAddress);
        }

        private void Run()
        {
            InputCommand command;

            do
            {
                command = _inputReceiver.GetNextInputCommand();

                if (!command.IsExitCommand)
                {
                    HandleCommand(command);
                }
            }
            while (command != null && !command.IsExitCommand);
        }

        private void HandleCommand(InputCommand command)
        {
            switch (command.CommandText)
            {
                case "getall":
                    ShowAllUserProfiles();
                    break;

                case "get":
                    {
                        string userIdString = command.GetParameterValue("userId");
                        string companyIdString = command.GetParameterValue("companyId");

                        if (!String.IsNullOrWhiteSpace(userIdString))
                        {
                            int userId;
                            if (Int32.TryParse(userIdString, out userId))
                            {
                                ShowSpecificUserProfile(userId);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{userIdString} is not a valid value for the 'userId' parameter");
                            }
                        }
                        else if (!String.IsNullOrWhiteSpace(companyIdString))
                        {
                            int companyId;
                            if (Int32.TryParse(companyIdString, out companyId))
                            {
                                ShowUserProfilesByCompany(companyId);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{companyIdString} is not a valid value for the 'companyId' parameter");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("You must specify either the 'userId' or 'companyId' parameter for the get command");
                        }
                    }
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{command.CommandText} is not a recognised command");
                    break;
            }

            Console.ResetColor();
        }

        private void ShowAllUserProfiles()
        {
            var task = _client.GetStringAsync("api/users");
            task.Wait();

            string result = task.Result;
            // TODO: Parse object from JSON.
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(result);
            Console.ResetColor();
        }

        private void ShowUserProfilesByCompany(int companyId)
        {
            var task = _client.GetStringAsync($"api/company/{companyId}/users");
            task.Wait();

            string result = task.Result;
            // TODO: Parse object from JSON.
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(result);
            Console.ResetColor();
        }

        private void ShowSpecificUserProfile(int userId)
        {
            var task = _client.GetStringAsync($"api/users/{userId}");
            task.Wait();

            string result = task.Result;
            // TODO: Parse object from JSON.
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(result);
            Console.ResetColor();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            var receiver = new ConsoleInputReceiver();

            try
            {
                Console.WriteLine("Starting client...");

                var program = new Program(receiver, GetApiAddress());
                program.Run();
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

        private static string GetApiAddress()
        {
            return ConfigurationManager.AppSettings["ApiAddress"];
        }
    }
}