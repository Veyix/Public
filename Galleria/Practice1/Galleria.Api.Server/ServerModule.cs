using Autofac;
using System.Configuration;

namespace Galleria.Api.Server
{
    public sealed class ServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<DatabaseContext>()
                .WithParameter("connectionString", GetConnectionString())
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserProfileRepository>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Galleria"].ConnectionString;
        }
    }
}