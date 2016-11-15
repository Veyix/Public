using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Galleria.Profiles.Api.Service
{
    public class CredentialVerificationProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            if (!IsValidUser(context.UserName, context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }
            else
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, context.UserName.ToLowerInvariant() == "admin" ? "Administrator" : "User"));

                context.Validated(identity);
            }

            return Task.FromResult<object>(null);
        }

        private static bool IsValidUser(string username, string password)
        {
            return username != null && password != null;
        }
    }
}