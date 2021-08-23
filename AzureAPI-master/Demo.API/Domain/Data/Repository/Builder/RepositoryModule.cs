using Autofac;
using Autofac.Integration.Mef;
using System;

namespace Demo.API.Domain.Data.Repository.Builder
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            base.Load(builder);
            builder.RegisterMetadataRegistrationSources();


            _ = builder
                .RegisterType<ClienteRepository>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            _ = builder
               .RegisterType<CentroRepository>()
               .InstancePerLifetimeScope()
               .PropertiesAutowired();


            _ = builder
                .RegisterType<EnderecoRepository>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            _ = builder
                .RegisterType<ProdutoRepository>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            _ = builder
                .RegisterType<UserRepository>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
        }
    }
}
