using Autofac;
using Autofac.Integration.WebApi;
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

            var container = CreateContainer();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            builder.UseAutofacMiddleware(container);
            builder.UseAutofacWebApi(config);
            builder.UseWebApi(config);
        }

        private IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(GetType().Assembly);

            return builder.Build();
        }
    }
}