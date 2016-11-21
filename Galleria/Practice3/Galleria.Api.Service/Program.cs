using DbUp;
using System;
using System.Configuration;
using System.ServiceProcess;

namespace Galleria.Api.Service
{
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

        private static void Run()
        {
            string hostAddress = ConfigurationManager.AppSettings["HostAddress"];
            var service = new HostService(hostAddress);

            if (Environment.UserInteractive)
            {
                service.RunLocal();
            }
            else
            {
                ServiceBase.Run(service);
            }
        }

        private static void UpgradeDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Galleria"].ConnectionString;
            EnsureDatabase.For.SqlDatabase(connectionString);

            Console.WriteLine($"Using connection string {connectionString}");
            var builder = DeployChanges.To.SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)
                .Build();

            if (!builder.IsUpgradeRequired())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Database is already up to date");
                Console.ResetColor();

                return;
            }

            Console.WriteLine("Starting upgrade...");
            var result = builder.PerformUpgrade();
            if (!result.Successful)
            {
                throw result.Error;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Database upgrade successful!");
            Console.ResetColor();
        }
    }
}