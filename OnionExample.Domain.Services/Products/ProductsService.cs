using System.Collections.Generic;
using OnionExample.Domain.DataProviders.Contracts.Products;
using OnionExample.Domain.Models.Common.Products;
using OnionExample.Domain.Services.Contracts.Products;

namespace OnionExample.Domain.Services.Products
{
    internal class ProductsService : IProductsService
    {
        private readonly IProductsDataProvider productsDataProvider;

        public ProductsService(IProductsDataProvider productsDataProvider)
        {
            this.productsDataProvider = productsDataProvider;
        }

        public int Create(Product product)
        {
            return this.productsDataProvider.Create(product);
        }

        public void Delete(int productId)
        {
            this.productsDataProvider.Delete(productId);
        }

        public IEnumerable<Product> GetAll()
        {
            return this.productsDataProvider.GetAll();
        }

        public Product GetById(int productId)
        {
            return this.productsDataProvider.GetById(productId);
        }

        public void Update(Product product)
        {
            this.productsDataProvider.Update(product);
        }
    }
}