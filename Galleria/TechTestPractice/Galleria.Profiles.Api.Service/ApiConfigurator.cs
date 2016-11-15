using Autofac;
using Autofac.Integration.WebApi;
using Galleria.Profiles.Infrastructure.AdoNet;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Linq;
using System.Net.Http.Formatting;
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

            // Setup the HTTP config and enable the attribute routing
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // Setup the dependency injection container and resolver using Autofac
            var container = CreateContainer();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            ConfigureSecurity(builder, container);
            ConfigureWebApi(builder, container, config);

            // JSON Formatting
            var formatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            formatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(GetType().Assembly);
            builder.RegisterAssemblyModules(
                typeof(InfrastructureModule).Assembly,
                typeof(ApiServiceModule).Assembly
            );

            return builder.Build();
        }

        private static void ConfigureSecurity(IAppBuilder builder, ILifetimeScope currentScope)
        {
            var provider = currentScope.Resolve<IOAuthAuthorizationServerProvider>();
            builder.UseOAuthAuthorizationServer(
                new OAuthAuthorizationServerOptions()
                {
                    AllowInsecureHttp = true,
                    Provider = provider,
                    TokenEndpointPath = new PathString("/api/login")
                }
            );

            builder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private static void ConfigureWebApi(IAppBuilder builder, IContainer container, HttpConfiguration config)
        {
            builder.UseAutofacMiddleware(container);
            builder.UseAutofacWebApi(config);
            builder.UseWebApi(config);
        }
    }
}