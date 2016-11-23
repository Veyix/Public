using Owin;
using System.Web.Http;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that configures the services on start up.
    /// </summary>
    public sealed class ServiceStartup
    {
        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="builder">The builder of the application.</param>
        public void Configuration(IAppBuilder builder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            builder.UseWebApi(config);
        }
    }
}