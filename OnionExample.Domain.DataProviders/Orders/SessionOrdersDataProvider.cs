using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnionExample.Core.DataProviders.Contracts.Orders;
using OnionExample.Core.Domain.Orders.Models;

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

        public int Create(Order order)
        {
            var orders = GetOrdersFromSession();
            int id = new Random().Next();

            orders.Add(order);

            return id;
        }

        public void Update(Order order)
        {
            Order sessionOrder = this.GetById(order.Id);

            sessionOrder.Status = order.Status;
            sessionOrder.Items = order.Items;
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
