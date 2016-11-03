using System.ServiceProcess;

namespace Galleria.Api.WindowsService
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            ServiceBase.Run(
                new[] { new UsersService() }
            );
        }
    }
}
