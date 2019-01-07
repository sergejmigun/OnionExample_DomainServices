using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnionExample.Core.DataProviders.Contracts.Products;
using OnionExample.Core.Domain.Products;

namespace OnionExample.Domain.DataProviders.Products
{
    internal class SessionProductsDataProvider : IProductsDataProvider
    {
        public IEnumerable<Product> GetAll()
        {
            return GetProductsFromSession();
        }

        public Product GetById(int productId)
        {
            return this.GetAll().First(x => x.Id == productId);
        }

        public int Create(Product product)
        {
            var products = GetProductsFromSession();
            int id = new Random().Next();

            products.Add(new Product
            {
                Id = id,
                Price = product.Price,
                Title = product.Title,
                Description = product.Description
            });

            return id;
        }

        public void Update(Product product)
        {
            Product currentProduct = this.GetById(product.Id);

            currentProduct.Price = product.Price;
            currentProduct.Title = product.Title;
            currentProduct.Description = product.Description;
        }

        public void Delete(int productId)
        {
            var products = GetProductsFromSession();

            products.Remove(products.First(x => x.Id == productId));
        }

        private static ICollection<Product> GetProductsFromSession()
        {
            ICollection<Product> products = HttpContext.Current.Session["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
                HttpContext.Current.Session["products"] = products;
            }

            return products;
        }
    }
}
