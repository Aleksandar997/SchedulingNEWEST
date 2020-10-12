using Scheduling.Contract.Models;
using Scheduling.Controller.Requests;
using Scheduling.Controller.Views;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling.Controller.Mappers
{
    public static class OrganizationUnitMapper
    {
        public static OrganizationUnit MapModel(OrganizationUnitRequest request) =>
            new OrganizationUnit()
            {
                OrganizationUnitId = request.OrganizationUnitId,
                Code = request.Code,
                Name = request.Name,
                Active = request.Active,
                BindScheduleToEmployee = request.BindScheduleToEmployee,
            };

        public static OrganizationUnitView MapView(OrganizationUnit model) =>
            new OrganizationUnitView()
            {
                OrganizationUnitId = model.OrganizationUnitId,
                Code = model.Code,
                Name = model.Name,
                Active = model.Active,
                BindScheduleToEmployee = model.BindScheduleToEmployee,
            };

        public static IEnumerable<OrganizationUnitView> MapView(IEnumerable<OrganizationUnit> model) =>
            model.Select(c => MapView(c));
    }
}
