using System.Collections.Generic;
using OnionExample.Core.Domain.Products;

namespace OnionExample.Core.DataProviders.Contracts.Products
{
    public interface IProductsDataProvider
    {
        IEnumerable<Product> GetAll();

        Product GetById(int productId);

        int Create(Product product);

        void Update(Product product);

        void Delete(int productId);
    }
}
