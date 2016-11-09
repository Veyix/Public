using System;
using System.Configuration;
using System.ServiceProcess;

namespace Galleria.Profiles.Api.Service
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
            string hostAddress = GetHostAddress();
            var service = new HostService(hostAddress);

            if (Environment.UserInteractive)
            {
                // Run the service locally as a console application
                service.RunLocal();
            }
            else
            {
                // Run the service as a Windows Service
                ServiceBase.Run(new ServiceBase[] { service });
            }
        }

        private static string GetHostAddress()
        {
            return ConfigurationManager.AppSettings["HostAddress"];
        }
    }
}