using OnionExample.Domain.Models.Common.Orders;
using OnionExample.Domain.Services.Contracts.Orders.Models;

namespace OnionExample.Domain.Services.Contracts.Orders
{
    public interface IOrderItemsService
    {
        void AddToOrder(OrderItemManagementData data);

        void DeleteFromOrder(OrderItemManagementData data);
    }
}
