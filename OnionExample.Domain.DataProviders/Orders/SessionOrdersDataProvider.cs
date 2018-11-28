using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.DataProviders.Contracts.Orders.Models;

namespace OnionExample.Domain.DataProviders.Orders
{
    internal class SessionOrdersDataProvider : IOrdersDataProvider
    {
        public IEnumerable<Order> GetAll()
        {
            return GetOrdersFromSession();
        }

        public Order GetById(int orderId)
        {
            return this.GetAll().First(x => x.Id == orderId);
        }

        public int Create(OrderCreationData creationData)
        {
            var orders = GetOrdersFromSession();
            int id = new Random().Next();

            orders.Add(new Order
            {
                Id = id,
                CreationDate = creationData.CreationDate,
                Customer = creationData.Customer,
                Items = creationData.Items.Select(x => x.ToOrderItem()).ToList(),
                Status = creationData.Status
            });

            return id;
        }

        public void Update(OrderUpdatingData updatingData)
        {
            Order order = this.GetById(updatingData.OrderId);

            order.Status = updatingData.Status;
            order.Items = updatingData.Items.Select(x => x.ToOrderItem()).ToList();
        }

        public void Delete(int orderId)
        {
            var orders = GetOrdersFromSession();
            var order = orders.First(x => x.Id == orderId);
            orders.Remove(order);
        }

        private static ICollection<Order> GetOrdersFromSession()
        {
            ICollection <Order> orders = HttpContext.Current.Session["orders"] as List<Order>;

            if (orders == null)
            {
                orders = new List<Order>();
                HttpContext.Current.Session["orders"] = orders;
            }

            return orders;
        }
    }
}
