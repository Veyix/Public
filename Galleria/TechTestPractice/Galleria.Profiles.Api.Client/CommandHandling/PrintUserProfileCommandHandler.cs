using Galleria.Profiles.ObjectModel;
using System;

namespace Galleria.Profiles.Api.Client.CommandHandling
{
    /// <summary>
    /// A class that provides common support for printing a user profile
    /// when handling a command.
    /// </summary>
    public abstract class PrintUserProfileCommandHandler : CommandHandler
    {
        protected void PrintUserProfile(UserProfile profile)
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
                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }
}