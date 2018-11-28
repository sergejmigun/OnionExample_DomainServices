using System.Linq;
using OnionExample.Domain.Models.Common.Orders;
using OnionExample.Domain.Services.Contracts.Orders.Models;
using Dal = OnionExample.Domain.DataProviders.Contracts.Orders.Models;

namespace OnionExample.Domain.Services.Orders
{
    public static class OrdersMapper
    {
        public static Order ToOrder(this Dal.Order order)
        {
            return new Order
            {
                Id = order.Id,
                CreationDate = order.CreationDate,
                Customer = order.Customer,
                Items = order.Items,
                Status = (OrderStatus)order.Status,
                Total = order.Items.Any() ? order.Items.Sum(x => x.Price * x.Quantity) : 0
            };
        }

        public static Dal.OrderCreationData ToDalOrderCreationData(this OrderCreationData creationData)
        {
            return new Dal.OrderCreationData
            {
                Customer = creationData.Customer,
                Items = creationData.Items.Select(x => x.ToDalOrderItemManagementData()).ToList()
            };
        }

        public static Dal.OrderUpdatingData ToDalOrderUpdatingData(this Dal.Order order)
        {
            return new Dal.OrderUpdatingData
            {
                OrderId = order.Id,
                Items = order.Items.Select(x => x.ToDalOrderItemManagementData(order.Id)).ToList(),
                Status = order.Status
            };
        }

        public static Dal.OrderItemManagementData ToDalOrderItemManagementData(this OrderItem orderItem, int orderId)
        {
            return new Dal.OrderItemManagementData
            {
                 OrderId = orderId,
                 ProductId = orderItem.ProductId.Value,
                 Price = orderItem.Price,
                 ProductTitle = orderItem.ProductTitle,
                 Quantity = orderItem.Quantity
            };
        }

        private static Dal.OrderItemManagementData ToDalOrderItemManagementData(this OrderItemManagementData data)
        {
            return new Dal.OrderItemManagementData
            {
                OrderId = data.OrderId,
                ProductId = data.ProductId,
                Quantity = data.Quantity
            };
        }
    }
}
