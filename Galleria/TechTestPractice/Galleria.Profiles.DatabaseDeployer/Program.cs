using DbUp;
using System;
using System.Configuration;

namespace Galleria.Profiles.DatabaseDeployer
{
    /// <summary>
    /// A class that provides the entry point for the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
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

            Console.ResetColor();

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.Write("Press any key to continue...");
                Console.ReadKey(intercept: true);
            }
        }

        private static void Run()
        {
            string connectionString = GetConnectionString();
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No connection string");

                return;
            }

            // Make sure the database exists
            Console.WriteLine($"Using connection string: {connectionString}...");
            Console.WriteLine("Ensuring database exists...");

            EnsureDatabase.For.SqlDatabase(connectionString);

            // Run all scripts embedded within the current assembly
            Console.WriteLine("Preparing the deployment...");
            var deployer = DeployChanges.To.SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)
                .LogToConsole()
                .Build();

            // Check to see if there are any changes to deploy
            if (!deployer.IsUpgradeRequired())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("The database is already up to date - no changes required");

                return;
            }

            // Perform the database upgrade
            Console.WriteLine("Performing database upgrade... ");
            Console.WriteLine("===============================");

            var result = deployer.PerformUpgrade();
            Console.WriteLine("===============================");

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Failed");
                Console.WriteLine(result.Error.Message);

                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
        }

        private static string GetConnectionString()
        {
            var settings = ConfigurationManager.ConnectionStrings["Galleria"];
            return settings?.ConnectionString;
        }
    }
}