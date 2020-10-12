using System;
using System.ComponentModel.DataAnnotations;
using Web.Attributes;

namespace Scheduling.Controller.Requests
{
    public class ScheduleRequest
    {
        public long ScheduleId { get; set; }
        [Required(ErrorMessage = "date_required")]
        public DateTime Date { get; set; }
        public long Id => ScheduleId;
        [ChildValidation(new string[] { "CustomerId" })]
        public CustomerRequest Customer { get; set; } = new CustomerRequest();
    }
}
