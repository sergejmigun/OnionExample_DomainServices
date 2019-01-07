﻿using OnionExample.Areas.Products.ApiModels;
using OnionExample.Core.Domain.Products;

namespace OnionExample.Areas.Products.Mappers
{
    public static class ProductsMapper
    {
        public static ProductModel ToProductModel(this Product product)
        {
            return new ProductModel
            {
                 Id = product.Id,
                 Title = product.Title,
                 Description = product.Description,
                 Price = product.Price
            };
        }

        public static Product ToProduct(this ProductSubmitModel productSubmitModel)
        {
            return new Product
            {
                Id = productSubmitModel.Id.GetValueOrDefault(),
                Title = productSubmitModel.Title,
                Description = productSubmitModel.Description,
                Price = productSubmitModel.Price
            };
        }
    }
}