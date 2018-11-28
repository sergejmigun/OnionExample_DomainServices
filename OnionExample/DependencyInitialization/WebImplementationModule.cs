using Autofac;
using OnionExample.Core.Cart.Services;

namespace OnionExample.DependencyInitialization
{
    public class WebImplementationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CartService>().As<ICartService>();
        }
    }
}