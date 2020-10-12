using Scheduling.Contract.Models;
using Scheduling.Controller.Requests;
using Scheduling.Controller.Views;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling.Controller.Mappers
{
    public static class PriceListTypeMapper
    {
        public static PricelistType MapModel(PricelistTypeRequest request) =>
            new PricelistType()
            {
                PricelistTypeId = request.PricelistTypeId,
                Name = request.Name,
                Code = request.Code,
                Active = request.Active
            };

        public static PricelistTypeView MapView(PricelistType model) =>
            new PricelistTypeView()
            {
                PricelistTypeId = model.PricelistTypeId,
                Name = model.Name,
                Code = model.Code,
                Active = model.Active
            };

        public static IEnumerable<PricelistTypeView> MapView(IEnumerable<PricelistType> model) =>
            model.Select(c => MapView(c));
    }
}
