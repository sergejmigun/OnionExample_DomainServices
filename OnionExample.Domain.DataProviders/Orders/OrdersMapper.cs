using OnionExample.Domain.DataProviders.Contracts.Orders.Models;
using OnionExample.Domain.Models.Common.Orders;

namespace OnionExample.Domain.DataProviders.Orders
{
    internal static class OrdersMapper
    {
        public static OrderItem ToOrderItem(this OrderItemManagementData data)
        {
            return new OrderItem
            {
                Price = data.Price,
                ProductId = data.ProductId,
                ProductTitle = data.ProductTitle,
                Quantity = data.Quantity
            };
        }
    }
}
