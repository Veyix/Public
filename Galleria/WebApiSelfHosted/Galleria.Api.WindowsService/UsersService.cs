using Microsoft.Owin.Hosting;
using System;
using System.ServiceProcess;

namespace Galleria.Api.WindowsService
{
    public partial class UsersService : ServiceBase
    {
        private IDisposable _api;

        public UsersService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Startup the service using OWIN
            _api = WebApp.Start<Startup>("http://localhost:60001/");
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
