using Common.Extensions;
using Scheduling.Contract.Models;
using Scheduling.Controller.Requests;
using Scheduling.Controller.Views;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling.Controller.Mappers
{
    public static class DocumentDetailMapper
    {

        public static DocumentDetail MapModel(DocumentDetailRequest request) =>
            new DocumentDetail()
            {
                DocumentDetailId = request.DocumentDetailId,
                Product = new Product() { ProductId = request.ProductId },
                Quantity = request.Quantity,
                Discount = request.Discount,
                Price = request.Price,
                Employee = new Employee() { EmployeeId = request.EmployeeId }
            };

        public static DocumentDetailView MapView(DocumentDetail model) =>
            new DocumentDetailView()
            {
                DocumentDetailId = model.DocumentDetailId,
                ProductId = model.Product.ProductId,
                Quantity = model.Quantity,
                Discount = model.Discount,
                Price = model.Price,
                PriceWithDiscount = model.PriceWithDiscount,
                EmployeeId = model.Employee.IfNull().EmployeeId
            };

        public static IEnumerable<DocumentDetailView> MapView(IEnumerable<DocumentDetail> model) =>
            model.Select(c => MapView(c));

    }
}
