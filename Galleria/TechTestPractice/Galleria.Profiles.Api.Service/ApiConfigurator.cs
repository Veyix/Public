using Owin;
using System;
using System.Web.Http;

namespace Galleria.Profiles.Api.Service
{
    /// <summary>
    /// A class that configures the API routes.
    /// </summary>
    public class ApiConfigurator
    {
        public void Configuration(IAppBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            builder.UseWebApi(config);
        }
    }
}