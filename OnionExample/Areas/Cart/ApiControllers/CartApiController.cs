using System.Web.Http;
using OnionExample.Areas.Cart.ApiModels;
using OnionExample.Areas.Orders.ApiModels;
using OnionExample.Core.Cart.Services;

namespace OnionExample.Areas.Orders.ApiControllers
{
    public class CartApiController : ApiController
    {
        private readonly ICartService cartService;

        public CartApiController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet]
        public CartModel GetCart()
        {
            return this.cartService.GetCart();
        }

        [HttpPut]
        public void AddProduct(int productId, int? quantity = null)
        {
            this.cartService.AddToCart(productId, quantity);
        }

        [HttpPut]
        public void UpdateCartItem(int productId, int quantity)
        {
            this.cartService.AddToCart(productId, quantity);
        }

        [HttpDelete]
        public void DeleteCartItem(int productId)
        {
            this.cartService.DeleteFromCart(productId);
        }

        [HttpPost]
        public void ProcessOrder(OrderSubmitModel submitModel)
        {
            this.cartService.ProcessOrder(submitModel);
        }
    }
}