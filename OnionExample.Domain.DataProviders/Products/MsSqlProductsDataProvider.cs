using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using OnionExample.Domain.DataProviders.Contracts.Products;
using OnionExample.Domain.DataProviders.Helpers;
using OnionExample.Domain.Models.Common.Products;

namespace OnionExample.Domain.DataProviders.Products
{
    internal class MsSqlProductsDataProvider : IProductsDataProvider
    {
        static MsSqlProductsDataProvider()
        {
            if (!MsSqlHelper.CheckDatabaseExists("OnionExample"))
            {
                MsSqlHelper.DeployDbInstance("OnionExample");
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string query = @"SELECT * FROM [dbo].[Products]";

                return db.Query<Product>(query);
            }
        }

        public Product GetById(int productId)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string query = @"SELECT * FROM [dbo].[Products] WHERE [Id] = @productId";

                return db.Query<Product>(query, new
                {
                    productId = productId
                }).FirstOrDefault();
            }
        }

        public int Create(Product product)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string query = @"INSERT INTO [dbo].[Products]([Title], [Description], [Price]) VALUES (@Title, @Description, @Price); SELECT CAST(SCOPE_IDENTITY() as int);";

                return db.Query<int>(query, product).First();
            }
        }

        public void Update(Product product)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string query = @"UPDATE [dbo].[Products] SET [Title] = @Title, [Description] = @Description, [Price] = @Price WHERE [Id] = @Id";

                db.Execute(query, product);
            }
        }

        public void Delete(int productId)
        {
            using (IDbConnection db = new SqlConnection(MsSqlHelper.GetConnectionString()))
            {
                string query = @"DELETE FROM [dbo].[Products] WHERE [Id] = @productId";

                db.Execute(query, new
                {
                    productId = productId
                });
            }
        }
    }
}
