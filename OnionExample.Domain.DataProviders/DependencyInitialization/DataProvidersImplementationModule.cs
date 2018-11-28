using Autofac;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.DataProviders.Contracts.Products;
using OnionExample.Domain.DataProviders.Orders;
using OnionExample.Domain.DataProviders.Products;

namespace OnionExample.Domain.DataProviders.DependencyInitialization
{
    public class DataProvidersImplementationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builder.RegisterType<SessionProductsDataProvider>().As<IProductsDataProvider>();
            // builder.RegisterType<MsSqlProductsDataProvider>().As<IProductsDataProvider>();
            builder.RegisterType<MongoDbProductsDataProvider>().As<IProductsDataProvider>();

            // builder.RegisterType<SessionOrdersDataProvider>().As<IOrdersDataProvider>();
            // builder.RegisterType<MsSqlOrdersDataProvider>().As<IOrdersDataProvider>();
            builder.RegisterType<MongoDbOrdersDataProvider>().As<IOrdersDataProvider>();
        }
    }
}