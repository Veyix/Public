using System;
using System.Web.Http;

namespace Galleria.Api.Service
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }
}