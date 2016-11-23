using Galleria.Api.Contract;
using Microsoft.Owin.Hosting;
using System;
using System.ServiceProcess;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that hosts the API service(s).
    /// </summary>
    public partial class HostService : ServiceBase
    {
        private readonly string _hostAddress;
        private IDisposable _api;

        /// <summary>
        /// Initializes a new instance of the <see cref="HostService"/> class.
        /// </summary>
        /// <param name="hostAddress">The address at which to host the service(s).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="hostAddress"/> is null or empty.</exception>
        public HostService(string hostAddress)
        {
            Verify.NotNullOrEmpty(hostAddress, nameof(hostAddress));

            _hostAddress = hostAddress;

            InitializeComponent();
        }

        /// <summary>
        /// Runs the service under a local context, allowing the use of the <see cref="Console"/> output.
        /// </summary>
        public void RunLocal()
        {
            Console.WriteLine("Starting the service...");
            OnStart(null);

            Console.WriteLine($"The service is running on {_hostAddress}. Press 'Q' to stop it.");
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
