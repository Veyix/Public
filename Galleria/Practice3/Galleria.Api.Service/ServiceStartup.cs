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
                    TokenEndpointPath = new PathString("/api/token"),
                    Provider = new CredentialVerificationProvider()
                }
            );

            builder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            builder.UseWebApi(config);
        }
    }
}