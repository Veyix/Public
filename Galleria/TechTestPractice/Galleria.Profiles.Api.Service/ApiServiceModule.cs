using Autofac;

namespace Galleria.Profiles.Api.Service
{
    /// <summary>
    /// A class that handles registration of components for the API Service project.
    /// </summary>
    public sealed class ApiServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Register the connection factory
            builder.RegisterType<ConnectionFactory>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Register the provider of credential verification
            builder.RegisterType<CredentialVerificationProvider>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}