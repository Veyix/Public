using DbUp;
using System;
using System.Configuration;

namespace Galleria.Api.Database
{
    public static class Program
    {
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
            string connectionString = ConfigurationManager.ConnectionStrings["Galleria"].ConnectionString;
            EnsureDatabase.For.SqlDatabase(connectionString);

            var builder = DeployChanges.To.SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)
                .Build();

            if (!builder.IsUpgradeRequired())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No changes detected");

                return;
            }

            var result = builder.PerformUpgrade();
            if (!result.Successful)
            {
                throw result.Error;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
        }
    }
}