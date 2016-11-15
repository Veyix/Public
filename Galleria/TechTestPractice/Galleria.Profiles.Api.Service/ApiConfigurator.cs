using Galleria.Profiles.Infrastructure.AdoNet;
using Galleria.Profiles.ObjectModel;
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

            // Authorization
            builder.UseOAuthAuthorizationServer(
                new OAuthAuthorizationServerOptions()
                {
                    AllowInsecureHttp = true,
                    Provider = new CredentialVerificationProvider(CreateRepository()),
                    TokenEndpointPath = new PathString("/api/login")
                }
            );

            // Authentication
            builder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // HTTP Config
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // Web API
            builder.UseWebApi(config);

            // JSON Formatting
            var formatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            formatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private static ISecurityUserRepository CreateRepository()
        {
            // TODO: DI this or add it into the OwinContext.
            var connection = ConnectionFactory.CreateConnection();
            return new SecurityUserRepository(connection);
        }
    }
}