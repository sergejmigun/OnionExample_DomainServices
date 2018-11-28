using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MongoDB.Driver;
using OnionExample.Domain.DataProviders.Contracts.Orders;
using OnionExample.Domain.DataProviders.Contracts.Orders.Models;
using OnionExample.Domain.DataProviders.Helpers;
using OnionExample.Domain.Models.Common.Orders;

namespace OnionExample.Domain.DataProviders.Orders
{
    internal class MongoDbOrdersDataProvider : IOrdersDataProvider
    {
        public IEnumerable<Order> GetAll()
        {
            return GetOrdersSet().FindSync(Builders<Order>.Filter.Empty).ToList();
        }

        public Order GetById(int orderId)
        {
            var filter = Builders<Order>.Filter.Where(x => x.Id == orderId);

            return GetOrdersSet().FindSync(filter).First();
        }

        public int Create(OrderCreationData order)
        {
            int id = new Random().Next();

            GetOrdersSet().InsertOne(new Order
            {
                Id = id,
                CreationDate = order.CreationDate,
                Customer = order.Customer,
                Items = order.Items.Select(x => new OrderItem
                {
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductTitle = x.ProductTitle,
                    Quantity = x.Quantity
                }).ToList(),
                Status = order.Status
            });

            return id;
        }

        public void Update(OrderUpdatingData order)
        {
            var database = MongoDbHelper.GetDatabase();

            var filter = Builders<Order>.Filter.Where(x => x.Id == order.OrderId);
            var update = Builders<Order>.Update
                .Set(x => x.Status, order.Status)
                .Set(x => x.Items, order.Items.Select(x => ToOrderItem(x)).ToList());

            GetOrdersSet().UpdateOne(filter, update);
        }

        public void Delete(int orderId)
        {
            var filter = Builders<Order>.Filter.Where(x => x.Id == orderId);
            GetOrdersSet().DeleteOne(filter);
        }

        private static OrderItem ToOrderItem(OrderItemManagementData data)
        {
            return new OrderItem
            {
                Price = data.Price,
                ProductId = data.ProductId,
                ProductTitle = data.ProductTitle,
                Quantity = data.Quantity
            };
        }

        private static IMongoCollection<Order> GetOrdersSet()
        {
            var database = MongoDbHelper.GetDatabase();

            return database.GetCollection<Order>("Orders");
        }
    }
}
