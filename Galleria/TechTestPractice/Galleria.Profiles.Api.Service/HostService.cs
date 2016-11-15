using Galleria.Support;
using Microsoft.Owin.Hosting;
using System;
using System.ServiceProcess;

namespace Galleria.Profiles.Api.Service
{
    public partial class HostService : ServiceBase
    {
        private readonly string _hostAddress;
        private IDisposable _api;

        /// <summary>
        /// Initializes a new instance of the <see cref="HostService"/> class.
        /// </summary>
        /// <param name="hostAddress">The URL to use when hosting the service.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="hostAddress"/> is null or empty.</exception>
        public HostService(string hostAddress)
        {
            Verify.NotNullOrEmpty(hostAddress, nameof(hostAddress));

            _hostAddress = hostAddress;

            InitializeComponent();
        }

        /// <summary>
        /// Runs the host service locally as a console application.
        /// </summary>
        public void RunLocal()
        {
            // Launch the service on the host address
            Console.WriteLine($"Starting the service on {_hostAddress}...");
            OnStart(null);

            // Wait for an exit command
            Console.WriteLine("Press 'Q' to stop the service...");
            while (!ShouldExit()) ;

            Console.WriteLine("Stopping the service...");
            OnStop();

            Console.WriteLine("Service stopped!");
        }

        private static bool ShouldExit()
        {
            var keyInfo = Console.ReadKey(intercept: true);
            return keyInfo.Key == ConsoleKey.Q;
        }

        protected override void OnStart(string[] args)
        {
            // Startup the API service and host at the given address
            _api = WebApp.Start<ApiConfigurator>(_hostAddress);
        }

        protected override void OnStop()
        {
            if (_api != null)
            {
                _api.Dispose();
            }
        }
    }
}
