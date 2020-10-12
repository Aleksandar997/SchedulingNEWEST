using System.ComponentModel.DataAnnotations;

namespace Scheduling.Controller.Requests
{
    public class CustomerRequest 
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "first_name_required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "last_name_required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "phone_number_required")]
        public string PhoneNumber { get; set; }
    }
}
