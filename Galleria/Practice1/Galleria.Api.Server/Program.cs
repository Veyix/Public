using System;
using System.Configuration;
using System.ServiceProcess;

namespace Galleria.Api.Server
{
    public static class Program
    {
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