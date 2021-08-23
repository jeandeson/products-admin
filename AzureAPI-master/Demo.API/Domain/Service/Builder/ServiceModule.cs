using Autofac;
using Autofac.Integration.Mef;
using System;

namespace Demo.API.Domain.Service.Builder
{
    public class ServiceModule : Module
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
                .RegisterType<BlobFileService>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            _ = builder
                .RegisterType<ProdutoService>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            _ = builder
               .RegisterType<ClienteService>()
               .InstancePerLifetimeScope()
               .PropertiesAutowired();

            _ = builder
               .RegisterType<CentroService>()
               .InstancePerLifetimeScope()
               .PropertiesAutowired();

            _ = builder
                .RegisterType<EnderecoService>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            _ = builder
                .RegisterType<UserService>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

        }
    }
}
