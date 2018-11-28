using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using OnionExample.Areas.Products.ApiModels;
using OnionExample.Domain.Services.Contracts.Products;
using OnionExample.Areas.Products.Mappers;

namespace OnionExample.Areas.Products.ApiControllers
{
    public class ProductsApiController : ApiController
    {
        private readonly IProductsService productsService;

        public ProductsApiController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet]
        public IEnumerable<ProductModel> GetProducts()
        {
            return this.productsService.GetAll().Select(x => x.ToProductModel());
        }

        [HttpGet]
        public ProductModel GetProduct(int id)
        {
            return this.GetProducts().First(x => x.Id == id);
        }

        [HttpPost]
        public int CreateProduct(ProductSubmitModel submitModel)
        {
            return this.productsService.Create(submitModel.ToProduct());
        }

        [HttpPut]
        public void EditProduct(ProductSubmitModel submitModel)
        {
            this.productsService.Update(submitModel.ToProduct());
        }

        [HttpDelete]
        public void DeleteProduct(int id)
        {
            this.productsService.Delete(id);
        }
    }
}