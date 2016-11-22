using Galleria.Api.Contract;
using Microsoft.Owin.Hosting;
using System;
using System.ServiceProcess;

namespace Galleria.Api.Server
{
    public partial class HostService : ServiceBase
    {
        private readonly string _hostAddress;
        private IDisposable _api;

        public HostService(string hostAddress)
        {
            Verify.NotNullOrEmpty(hostAddress, nameof(hostAddress));

            _hostAddress = hostAddress;

            InitializeComponent();
        }

        public void RunLocal()
        {
            try
            {
                OnStart(null);

                Console.WriteLine("Press 'Q' to exit...");
                while (!ShouldExit()) ;

                OnStop();
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
            }
        }
    }
}
