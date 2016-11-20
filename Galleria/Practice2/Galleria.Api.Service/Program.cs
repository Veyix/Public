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
            UpdateDatabase();

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

        private static void UpdateDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Galleria"].ConnectionString;
            EnsureDatabase.For.SqlDatabase(connectionString);

            Console.WriteLine("Using connection string " + connectionString);
            var builder = DeployChanges.To.SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)
                .Build();

            if (!builder.IsUpgradeRequired())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Database already up to date");
                Console.ResetColor();

                return;
            }

            Console.WriteLine("Upgrading database...");

            var result = builder.PerformUpgrade();
            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error.Message);
                Console.ResetColor();

                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Database upgraded successfully!");
            Console.ResetColor();
        }
    }
}