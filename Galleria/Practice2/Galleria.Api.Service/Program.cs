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
    }
}