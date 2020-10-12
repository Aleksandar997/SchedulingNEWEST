using Scheduling.Contract.Models;
using Scheduling.Controller.Requests;
using Scheduling.Controller.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Controller.Mappers
{
    public static class EmployeeMapper
    {
        public static Employee MapModel(EmployeeRequest request) =>
            new Employee()
            {
                //DocumentTypeId = request.DocumentTypeId,
                //Name = request.Name,
                //Code = request.Code
            };

        public static EmployeeView MapView(Employee model) =>
            new EmployeeView()
            {
                //DocumentTypeId = model.DocumentTypeId,
                //Name = model.Name,
                //Code = model.Code,
                //CodePath = model.CodePath
            };

        public static IEnumerable<EmployeeView> MapView(IEnumerable<Employee> model) =>
            model.Select(c => MapView(c));
    }
}
