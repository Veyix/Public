using Microsoft.Owin.Hosting;
using System;
using System.ServiceProcess;

namespace Galleria.Api.Service
{
    public partial class HostService : ServiceBase
    {
        private readonly string _hostAddress;
        private IDisposable _api;

        public HostService(string hostAddress)
        {
            _hostAddress = hostAddress;

            InitializeComponent();
        }

        public void RunLocal()
        {
            Console.WriteLine("Starting the service...");
            OnStart(null);

            Console.WriteLine("Press 'Q' to stop the service...");
            while (!ShouldExit()) ;

            Console.WriteLine("Stopping the service...");
            OnStop();
        }

        private static bool ShouldExit()
        {
            var keyInfo = Console.ReadKey(intercept: true);
            return keyInfo.Key == ConsoleKey.Q;
        }

        protected override void OnStart(string[] args)
        {
            _api = WebApp.Start<ServiceStartup>(new StartOptions(_hostAddress));
        }

        protected override void OnStop()
        {
            if (_api != null)
            {
                _api.Dispose();
                _api = null;
            }
        }
    }
}
