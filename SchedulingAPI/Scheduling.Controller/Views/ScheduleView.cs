using System;

namespace Scheduling.Controller.Views
{
    public class ScheduleView
    {
        public long? ScheduleId { get; set; }
        //[Required(ErrorMessage = "phone_number_required")]
        public string PhoneNumber { get; set; }
        //[Required(ErrorMessage = "date_required")]
        public DateTime Date { get; set; }
        //[Required(ErrorMessage = "customer_required")]
        public long CustomerId { get; set; }
        public string Employees { get; set; }
        //[ChildValidation]
        public long Id { get; set; }
        public CustomerView Customer { get; set; } = new CustomerView();
    }
}
