using System;
using System.Web.Http;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that provides an extension of the <see cref="AuthorizeAttribute"/> to define
    /// multiple valid roles to one class or method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeRolesAttribute"/> class.
        /// </summary>
        /// <param name="roles">A collection of valid roles.</param>
        public AuthorizeRolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }
}