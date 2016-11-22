using Autofac;
using Autofac.Integration.WebApi;
using Galleria.Api.Contract;
using Owin;
using System.Web.Http;

namespace Galleria.Api.Server
{
    public sealed class ServiceStartup
    {
        public void Configuration(IAppBuilder builder)
        {
            Verify.NotNull(builder, nameof(builder));

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            var container = CreateContainer();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            builder.UseAutofacMiddleware(container);
            builder.UseAutofacWebApi(config);
            builder.UseWebApi(config);
        }

        private static IContainer CreateContainer()
        {
            var assembly = typeof(ServiceStartup).Assembly;
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(assembly);
            builder.RegisterAssemblyModules(assembly);

            return builder.Build();
        }
    }
}