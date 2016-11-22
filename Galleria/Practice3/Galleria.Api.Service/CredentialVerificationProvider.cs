using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Galleria.Api.Service
{
    public sealed class CredentialVerificationProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Check that the user exists
            var securityUser = GetUser(context.UserName, context.Password);
            if (securityUser == null)
            {
                context.SetError("Invalid Credentials", "No user was found with the given username and password combination");
                context.Rejected();
            }
            else
            {
                var identity = new GenericIdentity(securityUser.Username, context.Request.MediaType);
                identity.AddClaim(new Claim(ClaimTypes.Name, securityUser.Username));
                identity.AddClaim(new Claim(ClaimTypes.Role, securityUser.Roles));

                context.Validated(identity);
            }

            return Task.FromResult<object>(null);
        }

        private static SecurityUser GetUser(string username, string password)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<SecurityUser>()
                    .Where(x => x.Username == username)
                    .Where(x => x.Password == password)
                    .FirstOrDefault();
            }
        }
    }
}