using Galleria.Profiles.ObjectModel;
using System;
using System.Configuration;
using System.Globalization;

namespace Galleria.Profiles.Api.Client
{
    /// <summary>
    /// A class that provides the main entry point for the application.
    /// </summary>
    public sealed class Program
    {
        private readonly IInputReceiver _inputReceiver;
        private readonly IUserProfileService _userProfileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        /// <param name="inputReceiver">A receiver of input commands.</param>
        /// <param name="userProfileService">A service providing access to the user profile API.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="inputReceiver"/> or
        /// <paramref name="userProfileService"/> is null.</exception>
        public Program(IInputReceiver inputReceiver, IUserProfileService userProfileService)
        {
            if (inputReceiver == null) throw new ArgumentNullException(nameof(inputReceiver));
            if (userProfileService == null) throw new ArgumentNullException(nameof(userProfileService));

            _inputReceiver = inputReceiver;
            _userProfileService = userProfileService;
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

                case "create":
                    CreateUserProfile(command);
                    break;

                case "update":
                    UpdateUserProfile(command);
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
            var profiles = _userProfileService.GetUserProfiles();
            foreach (var profile in profiles)
            {
                ShowUserProfile(profile);
            }
        }

        private void ShowUserProfilesByCompany(int companyId)
        {
            var profiles = _userProfileService.GetUserProfilesByCompanyId(companyId);
            foreach (var profile in profiles)
            {
                ShowUserProfile(profile);
            }
        }

        private void ShowSpecificUserProfile(int userId)
        {
            var profile = _userProfileService.GetUserProfile(userId);
            ShowUserProfile(profile);
        }

        private void ShowUserProfile(UserProfile profile)
        {
            if (profile == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No user profile");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($" --- USER {profile.Id} ---");
                Console.WriteLine($"Company Id: {profile.CompanyId}");
                Console.WriteLine($"Name: {profile.Title} {profile.Forename} {profile.Surname}");
                Console.WriteLine($"DOB: {profile.DateOfBirth:dd MMM yyyy}");
                Console.WriteLine("===========================");
            }

            Console.ResetColor();
        }

        private void CreateUserProfile(InputCommand command)
        {
            var profile = new UserProfile();

            // Extract all of the parameter values
            string companyIdValue = command.GetParameterValue("CompanyId");
            if (!String.IsNullOrWhiteSpace(companyIdValue))
            {
                profile.CompanyId = Convert.ToInt32(companyIdValue);
            }

            profile.Title = command.GetParameterValue("Title");
            profile.Forename = command.GetParameterValue("Forename");
            profile.Surname = command.GetParameterValue("Surname");

            string dateOfBirthValue = command.GetParameterValue("DateOfBirth");
            string dateFormat = command.GetParameterValue("DateFormat") ?? "yyyy-MM-dd";

            if (!String.IsNullOrWhiteSpace(dateOfBirthValue))
            {
                profile.DateOfBirth = DateTime.ParseExact(dateOfBirthValue, dateFormat, CultureInfo.InvariantCulture);
            }

            _userProfileService.CreateUserProfile(profile);
        }

        private void UpdateUserProfile(InputCommand command)
        {
            var profile = new UserProfile();

            // Extract all of the parameter values
            string userIdValue = command.GetParameterValue("UserId");
            if (!String.IsNullOrWhiteSpace(userIdValue))
            {
                profile.Id = Convert.ToInt32(userIdValue);
            }

            string companyIdValue = command.GetParameterValue("CompanyId");
            if (!String.IsNullOrWhiteSpace(companyIdValue))
            {
                profile.CompanyId = Convert.ToInt32(companyIdValue);
            }

            profile.Title = command.GetParameterValue("Title");
            profile.Forename = command.GetParameterValue("Forename");
            profile.Surname = command.GetParameterValue("Surname");

            string dateOfBirthValue = command.GetParameterValue("DateOfBirth");
            string dateFormat = command.GetParameterValue("DateFormat") ?? "yyyy-MM-dd";

            if (!String.IsNullOrWhiteSpace(dateOfBirthValue))
            {
                profile.DateOfBirth = DateTime.ParseExact(dateOfBirthValue, dateFormat, CultureInfo.InvariantCulture);
            }

            _userProfileService.UpdateUserProfile(profile);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            var receiver = new ConsoleInputReceiver();
            var service = new UserProfileService(GetApiAddress());

            try
            {
                Console.WriteLine("Starting client...");

                var program = new Program(receiver, service);
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