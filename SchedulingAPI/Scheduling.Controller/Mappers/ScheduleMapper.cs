using Scheduling.Contract.Models;
using Scheduling.Controller.Requests;
using Scheduling.Controller.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Controller.Mappers
{
    public static class ScheduleMapper
    {
        public static Schedule MapModel(ScheduleRequest request) =>
            new Schedule()
            {
                ScheduleId = request.ScheduleId,
                Date = request.Date,
                Customer = CustomerMapper.MapModel(request.Customer)
            };

        public static ScheduleView MapView(Schedule model) =>
            new ScheduleView()
            {
                ScheduleId = model.ScheduleId,
                Date = model.Date,
                Customer = CustomerMapper.MapView(model.Customer)
            };

        public static IEnumerable<ScheduleView> MapView(IEnumerable<Schedule> model) =>
            model.Select(c => MapView(c));
    }
}
