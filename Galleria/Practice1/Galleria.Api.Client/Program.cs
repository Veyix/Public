using System;
using System.Configuration;

namespace Galleria.Api.Client
{
    public static class Program
    {
        public static void Main()
        {
            try
            {
                string apiBaseAddress = ConfigurationManager.AppSettings["ApiBaseAddress"];
                using (var application = new ClientApplication(apiBaseAddress))
                {
                    application.Run();
                }
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
    }
}