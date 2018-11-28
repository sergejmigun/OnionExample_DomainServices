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
    internal class OrderItemsService : IOrderItemsService
    {
        private IOrdersDataProvider ordersDataProvider;
        private IProductsDataProvider productsDataProvider;

        public OrderItemsService(
            IOrdersDataProvider ordersDataProvider,
            IProductsDataProvider productsDataProvider)
        {
            this.ordersDataProvider = ordersDataProvider;
            this.productsDataProvider = productsDataProvider;
        }

        public void AddToOrder(OrderItemManagementData data)
        {
            Dal.Order order = this.ordersDataProvider.GetById(data.OrderId);
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

            this.ordersDataProvider.Update(order.ToDalOrderUpdatingData());
        }

        public void DeleteFromOrder(OrderItemManagementData data)
        {
            Dal.Order order = this.ordersDataProvider.GetById(data.OrderId);
            OrderItem item = order.Items.Where(x => x.ProductId == data.ProductId).FirstOrDefault();

            if (item != null)
            {
                order.Items.Remove(item);
                this.ordersDataProvider.Update(order.ToDalOrderUpdatingData());
            }
        }
    }
}
