using Autofac;
using OnionExample.Core.Services.Contracts.Orders;
using OnionExample.Core.Services.Contracts.Products;
using OnionExample.Domain.Services.Orders;
using OnionExample.Domain.Services.Products;

namespace OnionExample.Domain.Services.DependencyInitialization
{
    public class ServicesImplementationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductsService>().As<IProductsService>();
            builder.RegisterType<OrdersService>().As<IOrdersService>();
        }
    }
}