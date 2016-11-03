using Microsoft.Owin.Hosting;

namespace Galleria.Api.Console
{
    public static class Program
    {
        public static void Main()
        {
            try
            {
                using (WebApp.Start<Startup>("http://localhost:60000/"))
                {
                    // Wait until we are told to exit
                    while (!ShouldExit()) ;
                }
            }
            catch (System.Exception exception)
            {
                System.Console.ForegroundColor = System.ConsoleColor.Red;

                System.Console.WriteLine(exception.Message);
                System.Console.ResetColor();
            }
        }

        private static bool ShouldExit()
        {
            System.Console.Write("Press 'Q' to exit: ");

            var key = System.Console.ReadKey();
            return key.Key == System.ConsoleKey.Q;
        }
    }
}