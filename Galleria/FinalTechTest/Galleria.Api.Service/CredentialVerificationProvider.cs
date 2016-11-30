using Galleria.Api.Contract;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that provides verification of client credentials to grant or deny access to the API.
    /// </summary>
    public sealed class CredentialVerificationProvider : OAuthAuthorizationServerProvider
    {
        private readonly ISecurityUserRepository _securityUserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialVerificationProvider"/> class.
        /// </summary>
        /// <param name="securityUserRepository">A repository that manages instances of the <see cref="SecurityUser"/> class.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the given parameters are null.</exception>
        public CredentialVerificationProvider(ISecurityUserRepository securityUserRepository)
        {
            Verify.NotNull(securityUserRepository, nameof(securityUserRepository));

            _securityUserRepository = securityUserRepository;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Always validate the client as there is only one
            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = _securityUserRepository.GetSecurityUser(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("Invalid Credentials");
                context.Rejected();
            }
            else
            {
                // The user exists and has access to the system, so setup an identity to define the user going forward
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));

                context.Validated(identity);
            }

            return Task.FromResult<object>(null);
        }
    }
}