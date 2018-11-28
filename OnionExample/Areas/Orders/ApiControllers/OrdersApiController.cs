using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using OnionExample.Areas.Orders.ApiModels;
using OnionExample.Areas.Orders.Mappers;
using OnionExample.Domain.Services.Contracts.Orders;
using OnionExample.Domain.Services.Contracts.Orders.Models;

namespace OnionExample.Areas.Orders.ApiControllers
{
    public class OrdersApiController : ApiController
    {
        private readonly IOrdersService ordersService;
        private readonly IOrderItemsService orderItemsService;

        public OrdersApiController(
            IOrdersService ordersService,
            IOrderItemsService orderItemsService)
        {
            this.ordersService = ordersService;
            this.orderItemsService = orderItemsService;
        }

        [HttpGet]
        public IEnumerable<OrderModel> GetOrders()
        {
            return this.ordersService.GetAll().Select(x => x.ToOrderModel());
        }

        [HttpGet]
        public OrderModel GetOrder(int id)
        {
            return this.ordersService.GetById(id).ToOrderModel();
        }

        [HttpPut]
        public void CompleteOrder(int id)
        {
            this.ordersService.CompleteOrder(id);
        }

        [HttpDelete]
        public void DeleteOrder(int id)
        {
            this.ordersService.Delete(id);
        }

        [HttpPut]
        public void AddProduct(int orderId, int productId, int quantity)
        {
            this.orderItemsService.AddToOrder(new OrderItemManagementData
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity
            });
        }

        [HttpPut]
        public void UpdateProduct(int orderId, int productId, int quantity)
        {
            this.orderItemsService.AddToOrder(new OrderItemManagementData
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity
            });
        }

        [HttpDelete]
        public void DeleteProduct(int orderId, int productId)
        {
            this.orderItemsService.DeleteFromOrder(new OrderItemManagementData
            {
                OrderId = orderId,
                ProductId = productId
            });
        }
    }
}