using Galleria.Profiles.ObjectModel;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Galleria.Profiles.Api.Service
{
    /// <summary>
    /// A class that provides verification logic for user credentials.
    /// </summary>
    public class CredentialVerificationProvider : OAuthAuthorizationServerProvider
    {
        private readonly ISecurityUserRepository _securityUserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialVerificationProvider"/> class.
        /// </summary>
        /// <param name="securityUserRepository">A repository that manages instances of the <see cref="SecurityUser"/> class.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="securityUserRepository"/> is null.</exception>
        public CredentialVerificationProvider(ISecurityUserRepository securityUserRepository)
        {
            if (securityUserRepository == null) throw new ArgumentNullException(nameof(securityUserRepository));

            _securityUserRepository = securityUserRepository;
        }

        /// <summary>
        /// Validate the authentication of the API client.
        /// </summary>
        /// <param name="context">The context to use when validating the client.</param>
        /// <returns>A task to allow asynchronous execution of the method.</returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Validates the credentials of the user and grants or denies access to the system.
        /// </summary>
        /// <param name="context">The context containing the user credentials.</param>
        /// <returns>A task to allow asynchronous execution of the method.</returns>
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // TODO: Determine if this is actually needed. If so, research what it actually does.
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            if (!IsValidUser(context.UserName, context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }
            else
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, GetUserRole(context.UserName)));

                context.Validated(identity);
            }

            return Task.FromResult<object>(null);
        }

        private bool IsValidUser(string username, string password)
        {
            if (username == null || password == null)
            {
                return false;
            }

            // Find the user using the given username and compare the passwords
            // TODO: Really we should be using some kind of password hashing mechanism here so we aren't using plain text passwords.
            var user = _securityUserRepository.GetByUsername(username);
            return user != null && user.Password == password;
        }

        private static string GetUserRole(string username)
        {
            return username.ToLowerInvariant() == "admin"
                ? SecurityRoles.Administrator
                : SecurityRoles.BasicUser;
        }
    }
}