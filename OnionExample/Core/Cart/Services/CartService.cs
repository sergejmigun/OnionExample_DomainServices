using System.Linq;
using System.Web;
using OnionExample.Areas.Cart.ApiModels;
using OnionExample.Areas.Orders.ApiModels;
using OnionExample.Core.Domain.Orders;
using OnionExample.Core.Services.Contracts.Orders.Models;
using OnionExample.Core.Services.Contracts.Orders;
using OnionExample.Core.Services.Contracts.Products;

namespace OnionExample.Core.Cart.Services
{
    internal class CartService : ICartService
    {
        private readonly IProductsService productsService;
        private readonly IOrdersService ordersService;

        static CartService()
        {
            HttpContext.Current.Session["cart"] = new CartModel();
        }

        public CartService(
            IProductsService productsService,
            IOrdersService ordersService)
        {
            this.productsService = productsService;
            this.ordersService = ordersService;
        }

        public CartModel GetCart()
        {
            return HttpContext.Current.Session["cart"] as CartModel;
        }

        public void AddToCart(int productId, int? quantity)
        {
            CartModel cart = this.GetCart();
            var productInCart = cart.Items.FirstOrDefault(x => x.ProductId == productId);

            if (productInCart == null)
            {
                var product = this.productsService.GetById(productId);

                productInCart = new CartItemModel
                {
                    Price = product.Price,
                    ProductId = product.Id,
                    ProductName = product.Title
                };

                cart.Items.Add(productInCart);
            }

            if (quantity.HasValue)
            {
                productInCart.Quantity = quantity.Value;
            }
            else
            {
                productInCart.Quantity++;
            }
        }

        public void DeleteFromCart(int productId)
        {
            CartModel cart = this.GetCart();
            var productInCart = cart.Items.FirstOrDefault(x => x.ProductId == productId);
            cart.Items.Remove(productInCart);
        }

        public void ProcessOrder(OrderSubmitModel submitModel)
        {
            CartModel cart = this.GetCart();

            this.ordersService.Create(new OrderManagementData
            {
                Customer = new OrderCustomer
                {
                    Name = submitModel.Customer.Name,
                    Phone = submitModel.Customer.Phone
                },
                Items = submitModel.Items.Select(x => new OrderItemManagementData
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            });

            cart.Items.Clear();
        }
    }
}