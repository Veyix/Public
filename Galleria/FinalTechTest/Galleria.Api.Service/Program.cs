using DbUp;
using Galleria.Api.Contract;
using System;
using System.Configuration;
using System.ServiceProcess;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that provides the main entry point for the application.
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
                UpgradeDatabase();
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
        }

        private static void UpgradeDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Galleria"]?.ConnectionString;
            Verify.NotNullOrEmpty(connectionString, nameof(connectionString));

            Console.WriteLine($"Checking database {connectionString}...");
            EnsureDatabase.For.SqlDatabase(connectionString);

            var deployer = DeployChanges.To.SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)
                .Build();

            if (!deployer.IsUpgradeRequired())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Database is already up to date");
                Console.ResetColor();
            }

            Console.WriteLine("Upgrading the database...");

            var result = deployer.PerformUpgrade();
            if (!result.Successful)
            {
                throw result.Error;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Upgrade successful!");
            Console.ResetColor();
        }

        private static void Run()
        {
            string hostAddress = ConfigurationManager.AppSettings["HostAddress"];
            if (String.IsNullOrWhiteSpace(hostAddress))
            {
                throw new InvalidOperationException("No host address found");
            }

            var service = new HostService(hostAddress);
            if (Environment.UserInteractive)
            {
                try
                {
                    // We are running as a console application, so run the service locally
                    service.RunLocal();
                }
                finally
                {
                    service.Dispose();
                }
            }
            else
            {
                // We are running as a Windows Service, so run the service through the ServiceBase mechanism
                ServiceBase.Run(service);
            }
        }
    }
}