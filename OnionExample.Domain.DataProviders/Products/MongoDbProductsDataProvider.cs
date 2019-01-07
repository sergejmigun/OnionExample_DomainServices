using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using OnionExample.Core.DataProviders.Contracts.Products;
using OnionExample.Core.Domain.Products;
using OnionExample.Domain.DataProviders.Helpers;

namespace OnionExample.Domain.DataProviders.Products
{
    internal class MongoDbProductsDataProvider : IProductsDataProvider
    {
        public IEnumerable<Product> GetAll()
        {
            return GetProductsSet().FindSync(Builders<Product>.Filter.Empty).ToList();
        }

        public Product GetById(int productId)
        {
            var filter = Builders<Product>.Filter.Where(x => x.Id == productId);

            return GetProductsSet().FindSync(filter).First();
        }

        public int Create(Product product)
        {
            int id = new Random().Next();
            product.Id = id;

            GetProductsSet().InsertOne(product);

            return id;
        }

        public void Update(Product product)
        {
            var database = MongoDbHelper.GetDatabase();

            var filter = Builders<Product>.Filter.Where(x => x.Id == product.Id);
            var update = Builders<Product>.Update
                .Set(x => x.Description, product.Description)
                .Set(x => x.Price, product.Price)
                .Set(x => x.Title, product.Title);

            GetProductsSet().UpdateOne(filter, update);
        }

        public void Delete(int productId)
        {
            var filter = Builders<Product>.Filter.Where(x => x.Id == productId);
            GetProductsSet().DeleteOne(filter);
        }

        private static IMongoCollection<Product> GetProductsSet()
        {
            var database = MongoDbHelper.GetDatabase();

            return database.GetCollection<Product>("Products");
        }
    }
}
