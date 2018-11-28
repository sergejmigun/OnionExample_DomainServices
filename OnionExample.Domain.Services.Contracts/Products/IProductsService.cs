using System.Collections.Generic;
using OnionExample.Domain.Models.Common.Products;

namespace OnionExample.Domain.Services.Contracts.Products
{
    public interface IProductsService
    {
        IEnumerable<Product> GetAll();

        Product GetById(int productId);

        int Create(Product product);

        void Update(Product product);

        void Delete(int productId);
    }
}
