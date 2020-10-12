using Scheduling.Contract.Models;
using Scheduling.Controller.Requests;
using Scheduling.Controller.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Controller.Mappers
{
    public static class ProductMapper
    {
        public static Product MapModel(ProductRequest request) =>
            new Product()
            {
                ProductId = request.ProductId,
                Code = request.Code,
                Name = request.Name,
                Image = request.Image,
                Active = request.Active,
            };

        public static ProductView MapView(Product model) =>
            new ProductView()
            {
                ProductId = model.ProductId,
                Code = model.Code,
                Name = model.Name,
                Image = model.Image,
                Active = model.Active,
            };

        public static IEnumerable<ProductView> MapView(IEnumerable<Product> model) =>
            model.Select(c => MapView(c));
    }
}
