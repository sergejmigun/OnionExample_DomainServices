using System.Collections.Generic;
using OnionExample.Core.Domain.Orders.Models;
using OnionExample.Core.Services.Contracts.Orders.Models;

namespace OnionExample.Core.Services.Contracts.Orders
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetAll();

        Order GetById(int orderId);

        int Create(OrderManagementData order);

        void CompleteOrder(int orderId);

        void AddToOrder(OrderItemManagementData data);

        void DeleteFromOrder(OrderItemManagementData data);

        void Delete(int orderId);
    }
}
