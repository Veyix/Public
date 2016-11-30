using Galleria.Api.Contract;
using System;
using System.Configuration;

namespace Galleria.Api.Client
{
    /// <summary>
    /// A class that provides the main entry point for the program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point of the program.
        /// </summary>
        public static void Main()
        {
            try
            {
                Run();
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

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(intercept: true);
            }
        }

        private static void Run()
        {
            string serviceAddress = ConfigurationManager.AppSettings["ServiceAddress"];
            Verify.NotNullOrEmpty(serviceAddress, nameof(serviceAddress));

            string username = ConfigurationManager.AppSettings["Username"];
            Verify.NotNullOrEmpty(username, nameof(username));

            string password = ConfigurationManager.AppSettings["Password"];
            Verify.NotNullOrEmpty(password, nameof(password));

            using (var application = new ClientApplication(serviceAddress))
            {
                application.Run(username, password);
            }
        }
    }
}