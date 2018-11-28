using System.Collections.Generic;
using OnionExample.Domain.DataProviders.Contracts.Orders.Models;

namespace OnionExample.Domain.DataProviders.Contracts.Orders
{
    public interface IOrdersDataProvider
    {
        IEnumerable<Order> GetAll();

        Order GetById(int orderId);

        int Create(OrderCreationData order);

        void Update(OrderUpdatingData order);

        void Delete(int orderId);
    }
}