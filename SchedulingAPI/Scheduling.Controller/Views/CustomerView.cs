using System;

namespace Scheduling.Controller.Views
{
    public class CustomerView
    {
        public int? CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerName => $"{FirstName} {LastName}";
        public string PhoneNumber { get; set; }
        public Guid CompanyId { get; set; }
        public UserView User { get; set; }
        public int? Id => CustomerId;
    }
}
