using System;
using System.Configuration;

namespace Galleria.Profiles.Api.Client
{
    /// <summary>
    /// A class that provides the main entry point for the application.
    /// </summary>
    public sealed class Program
    {
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

                var program = new ClientApplication(receiver, service);
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

                service.Dispose();
            }
        }

        private static string GetApiAddress()
        {
            return ConfigurationManager.AppSettings["ApiAddress"];
        }
    }
}