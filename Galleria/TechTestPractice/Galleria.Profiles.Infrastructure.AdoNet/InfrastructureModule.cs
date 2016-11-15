using Autofac;
using System.Linq;

namespace Galleria.Profiles.Infrastructure.AdoNet
{
    /// <summary>
    /// A class that handles registration of components for the ADO.NET Infrastructure project.
    /// </summary>
    public sealed class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Register all repository instances against their interfaces
            var repositoryTypes = ThisAssembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.Name.EndsWith("Repository"))
                .ToArray();

            builder.RegisterTypes(repositoryTypes)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}