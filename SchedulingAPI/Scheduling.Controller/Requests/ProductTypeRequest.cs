using System;
using System.ComponentModel.DataAnnotations;

namespace Scheduling.Controller.Requests
{
    public class ProductTypeRequest
    {
        public int ProductTypeId { get; set; }
        [Required(ErrorMessage = "code_required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "name_required")]
        public string Name { get; set; }
        public int Id => ProductTypeId;
        public bool Active { get; set; }
    }
}