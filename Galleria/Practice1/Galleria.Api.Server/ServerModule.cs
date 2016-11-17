using Autofac;

namespace Galleria.Api.Server
{
    public sealed class ServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<UserProfileRepository>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}