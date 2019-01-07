using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using OnionExample.Core.DataProviders.Contracts.Orders;
using OnionExample.Core.Domain.Orders;
using OnionExample.Core.Domain.Orders.Models;
using OnionExample.Domain.DataProviders.Helpers;

namespace OnionExample.Domain.DataProviders.Orders
{
    internal class MsSqlOrdersDataProvider : IOrdersDataProvider
    {
        static MsSqlOrdersDataProvider()
        {
            if (!MsSqlHelper.CheckDatabaseExists("OnionExample"))
            {
                MsSqlHelper.DeployDbInstance("OnionExample");
            }
        }

        public IEnumerable<Order> GetAll()
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string ordersQuery = @"SELECT * FROM [dbo].[Orders]";

                var dbOrders = db.Query<dynamic>(ordersQuery);

                return dbOrders.Select(x => GetOrder(db, x)).Cast<Order>().ToList();
            }
        }

        public Order GetById(int orderId)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string ordersQuery = @"SELECT * FROM [dbo].[Orders] WHERE [Id] = @orderId";

                var dbOrder = db.Query<dynamic>(ordersQuery, new
                {
                    orderId = orderId
                }).First();

                return GetOrder(db, dbOrder);
            }
        }

        public int Create(Order order)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string insertOrderQuery = @"INSERT INTO [dbo].[Orders]([CreationDate], [CustomerName], [CustomerPhone], [Status]) VALUES (@CreationDate, @CustomerName, @CustomerPhone, @Status); SELECT CAST(SCOPE_IDENTITY() as int);";

                int orderId = db.Query<int>(insertOrderQuery, new
                {
                    CreationDate = order.CreationDate,
                    CustomerName = order.Customer.Name,
                    CustomerPhone = order.Customer.Phone,
                    Status = order.Status
                }).First();

                InsertOrderItems(db, order.Items, orderId);

                return orderId;
            }
        }

        public void Update(Order order)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string updateOrderQuery = @"UPDATE [dbo].[Orders] SET [Status] = @Status WHERE [Id] = @OrderId";

                db.Execute(updateOrderQuery, new
                {
                    Status = order.Status,
                    OrderId = order.Id
                });

                string deleteItemsQuery = "DELETE FROM [dbo].[OrderItems] WHERE [OrderId] = @OrderId";

                db.Execute(deleteItemsQuery, new
                {
                    OrderId = order.Id
                });

                InsertOrderItems(db, order.Items, order.Id);
            }
        }

        public void Delete(int orderId)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string query = "DELETE FROM [dbo].[Orders] WHERE [Id] = @OrderId";

                db.Execute(query, new
                {
                    OrderId = orderId
                });
            }
        }

        private static Order GetOrder(IDbConnection db, dynamic dbOrder)
        {
            return new Order
            {
                Id = dbOrder.Id,
                CreationDate = dbOrder.CreationDate,
                Status = dbOrder.Status,
                Customer = new OrderCustomer
                {
                    Name = dbOrder.CustomerName,
                    Phone = dbOrder.CustomerPhone
                },
                Items = GetOrderItems(db, dbOrder.Id)
            };
        }

        private static ICollection<OrderItem> GetOrderItems(IDbConnection db, int orderId)
        {
            string orderItemsQuery = @"SELECT * FROM [dbo].[OrderItems] WHERE [OrderId] = @orderId";

            return db.Query<OrderItem>(orderItemsQuery, new
            {
                orderId = orderId
            }).ToList();
        }

        private static void InsertOrderItems(IDbConnection db, IEnumerable<OrderItem> items, int orderId)
        {
            string insertOrderItemQuery = @"INSERT INTO [dbo].[OrderItems]([OrderId], [ProductId], [ProductTitle], [Price], [Quantity]) VALUES (@OrderId, @ProductId, @ProductTitle, @Price, @Quantity)";

            foreach (OrderItem item in items)
            {
                db.Execute(insertOrderItemQuery, new
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    ProductTitle = item.ProductTitle,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }
        }
    }
}
