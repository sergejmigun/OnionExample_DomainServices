using System.Collections.Generic;
using OnionExample.Core.Domain.Orders.Models;

namespace OnionExample.Core.DataProviders.Contracts.Orders
{
    public interface IOrdersDataProvider
    {
        IEnumerable<Order> GetAll();

        Order GetById(int orderId);

        int Create(Order order);

        void Update(Order order);

        void Delete(int orderId);
    }
}