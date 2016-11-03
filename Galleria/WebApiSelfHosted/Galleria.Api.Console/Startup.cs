using Owin;
using System.Web.Http;

namespace Galleria.Api.Console
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            builder.UseWebApi(config);
        }
    }
}