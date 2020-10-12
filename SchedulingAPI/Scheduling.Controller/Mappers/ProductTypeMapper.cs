using Scheduling.Contract.Models;
using Scheduling.Controller.Requests;
using Scheduling.Controller.Views;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling.Controller.Mappers
{
    public static class ProductTypeMapper
    {
        public static ProductType MapModel(ProductTypeRequest request) =>
            new ProductType()
            {
                ProductTypeId = request.ProductTypeId,
                Code = request.Code,
                Name = request.Name,
                Active = request.Active,
            };

        public static ProductTypeView MapView(ProductType model) =>
            new ProductTypeView()
            {
                ProductTypeId = model.ProductTypeId,
                Code = model.Code,
                Name = model.Name,
                Active = model.Active,
            };

        public static IEnumerable<ProductTypeView> MapView(IEnumerable<ProductType> model) =>
            model.Select(c => MapView(c));
    }
}
