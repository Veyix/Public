using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
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

            builder.UseOAuthAuthorizationServer(
                new OAuthAuthorizationServerOptions()
                {
                    TokenEndpointPath = new PathString("/api/login"),
                    Provider = new CredentialVerificationProvider(),
                    AllowInsecureHttp = true
                }
            );

            builder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            builder.UseWebApi(config);

            // JSON Formatting
            //var formatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //formatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}