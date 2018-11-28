using OnionExample.Areas.Cart.ApiModels;
using OnionExample.Areas.Orders.ApiModels;

namespace OnionExample.Core.Cart.Services
{
    public interface ICartService
    {
        CartModel GetCart();

        void AddToCart(int productId, int? quantity);

        void DeleteFromCart(int productId);

        void ProcessOrder(OrderSubmitModel submitModel);
    }
}