using Autofac;
using System.Linq;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that handles registration of components for the service.
    /// </summary>
    public sealed class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Register the data context
            builder.RegisterType<DatabaseContext>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Register repositories
            var repositoryTypes = ThisAssembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.Name.EndsWith("Repository"))
                .ToArray();

            builder.RegisterTypes(repositoryTypes)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // Register the authentication server
            builder.RegisterType<CredentialVerificationProvider>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}