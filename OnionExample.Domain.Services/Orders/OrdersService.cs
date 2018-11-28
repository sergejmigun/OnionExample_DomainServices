using System;
using System.Collections.Generic;
using System.Linq;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.DataProviders.Contracts.Products;
using OnionExample.Domain.Models.Common.Orders;
using OnionExample.Domain.Models.Common.Products;
using OnionExample.Domain.Services.Contracts.Orders;
using OnionExample.Domain.Services.Contracts.Orders.Models;
using Dal = OnionExample.Domain.DataProviders.Contracts.Orders.Models;

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
            return this.ordersDataProvider.GetAll().Select(x => x.ToOrder()).ToList();
        }

        public Order GetById(int orderId)
        {
            return this.ordersDataProvider.GetById(orderId).ToOrder();
        }

        public int Create(OrderCreationData order)
        {
            Dal.OrderCreationData dalOrderData = order.ToDalOrderCreationData();

            dalOrderData.CreationDate = DateTime.Now;
            dalOrderData.Status = (short)OrderStatus.Pending;

            foreach (Dal.OrderItemManagementData item in dalOrderData.Items)
            {
                Product product = this.productsDataProvider.GetById(item.ProductId);

                item.ProductTitle = product.Title;
                item.Price = product.Price;
            }

            return this.ordersDataProvider.Create(dalOrderData);
        }

        public void CompleteOrder(int orderId)
        {
            Order order = this.GetById(orderId);

            this.ordersDataProvider.Update(new Dal.OrderUpdatingData
            {
                OrderId = orderId,
                Status = (short)OrderStatus.Processed,
                Items = order.Items.Select(x => x.ToDalOrderItemManagementData(orderId))
            });
        }

        public void Delete(int orderId)
        {
            this.ordersDataProvider.Delete(orderId);
        }
    }
}
