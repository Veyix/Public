using Owin;
using System.Web.Http;

namespace Galleria.Api.Service
{
    public sealed class ServiceStartup
    {
        public void Configuration(IAppBuilder builder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            builder.UseWebApi(config);
        }
    }
}