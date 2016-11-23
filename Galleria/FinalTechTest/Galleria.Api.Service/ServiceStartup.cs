using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Security.OAuth;
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

            ConfigureSecurity(builder, container);

            builder.UseAutofacMiddleware(container);
            builder.UseAutofacWebApi(config);
            builder.UseWebApi(config);
        }

        private IContainer CreateContainer()
        {
            var currentAssembly = GetType().Assembly;
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(currentAssembly);
            builder.RegisterAssemblyModules(currentAssembly);

            return builder.Build();
        }

        private void ConfigureSecurity(IAppBuilder builder, ILifetimeScope scope)
        {
            // Use the current lifetime scope to resolve the authorization provider
            var provider = scope.Resolve<IOAuthAuthorizationServerProvider>();
            builder.UseOAuthAuthorizationServer(
                new OAuthAuthorizationServerOptions()
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new Microsoft.Owin.PathString("/api/login"),
                    Provider = provider
                }
            );

            builder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}