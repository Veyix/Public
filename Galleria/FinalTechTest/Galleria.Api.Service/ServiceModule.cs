using Autofac;

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

            // Register the authentication server
            builder.RegisterType<CredentialVerificationProvider>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}