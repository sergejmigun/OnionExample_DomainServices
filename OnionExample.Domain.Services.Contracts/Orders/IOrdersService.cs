using System.Collections.Generic;
using OnionExample.Domain.Services.Contracts.Orders.Models;

namespace OnionExample.Domain.Services.Contracts.Orders
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetAll();

        Order GetById(int orderId);

        int Create(OrderCreationData order);

        void CompleteOrder(int orderId);

        void Delete(int orderId);
    }
}
