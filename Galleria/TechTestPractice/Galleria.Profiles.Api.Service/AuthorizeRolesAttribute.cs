using System;
using System.Web.Http;

namespace Galleria.Profiles.Api.Service
{
    /// <summary>
    /// A class that allows multiple roles to be defined using constants for
    /// use in authorization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeRolesAttribute"/> class.
        /// </summary>
        /// <param name="roles">A collection of roles to which to restrict authorization of the attributed target.</param>
        public AuthorizeRolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }
}