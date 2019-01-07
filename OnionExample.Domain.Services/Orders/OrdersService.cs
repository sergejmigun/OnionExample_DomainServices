using System;
using System.Collections.Generic;
using System.Linq;
using OnionExample.Core.DataProviders.Contracts.Orders;
using OnionExample.Core.DataProviders.Contracts.Products;
using OnionExample.Core.Domain.Orders;
using OnionExample.Core.Domain.Orders.Models;
using OnionExample.Core.Domain.Products;
using OnionExample.Core.Services.Contracts.Orders.Models;
using OnionExample.Core.Services.Contracts.Orders;

namespace OnionExample.Domain.Services.Orders
{
    internal class OrdersService : IOrdersService
    {
        private readonly IOrdersDataProvider ordersDataProvider;
        private readonly IProductsDataProvider productsDataProvider;

        public OrdersService(
            IOrdersDataProvider ordersDataProvider,
            IProductsDataProvider productsDataProvider)
        {
            this.ordersDataProvider = ordersDataProvider;
            this.productsDataProvider = productsDataProvider;
        }

        public IEnumerable<Order> GetAll()
        {
            return this.ordersDataProvider.GetAll();
        }

        public Order GetById(int orderId)
        {
            return this.ordersDataProvider.GetById(orderId);
        }

        public int Create(OrderManagementData orderData)
        {
            var order = new Order
            {
                CreationDate = DateTime.Now,
                Status = (short)OrderStatus.Pending,
                Items = new List<OrderItem>(),
                Customer = orderData.Customer
            };

            foreach (OrderItemManagementData item in orderData.Items)
            {
                Product product = this.productsDataProvider.GetById(item.ProductId);

                order.Items.Add(new OrderItem
                {
                    ProductTitle = product.Title,
                    Price = product.Price
                });
            }

            return this.ordersDataProvider.Create(order);
        }

        public void CompleteOrder(int orderId)
        {
            Order order = this.GetById(orderId);

            order.Status = (short)OrderStatus.Processed;
        }

        public void Delete(int orderId)
        {
            this.ordersDataProvider.Delete(orderId);
        }

        public void AddToOrder(OrderItemManagementData data)
        {
            Order order = this.ordersDataProvider.GetById(data.OrderId);
            OrderItem item = order.Items.Where(x => x.ProductId == data.ProductId).FirstOrDefault();

            if (item == null)
            {
                Product product = this.productsDataProvider.GetById(data.ProductId);

                order.Items.Add(new OrderItem
                {
                    ProductId = data.ProductId,
                    Price = product.Price,
                    ProductTitle = product.Title,
                    Quantity = data.Quantity
                });
            }
            else
            {
                item.Quantity = data.Quantity;
            }

            this.ordersDataProvider.Update(order);
        }

        public void DeleteFromOrder(OrderItemManagementData data)
        {
            Order order = this.ordersDataProvider.GetById(data.OrderId);
            OrderItem item = order.Items.Where(x => x.ProductId == data.ProductId).FirstOrDefault();

            if (item != null)
            {
                order.Items.Remove(item);
                this.ordersDataProvider.Update(order);
            }
        }
    }
}
