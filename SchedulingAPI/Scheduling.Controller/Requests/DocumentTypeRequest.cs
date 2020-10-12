using System.ComponentModel.DataAnnotations;

namespace Scheduling.Controller.Requests
{
    public class DocumentTypeRequest
    {
        public int DocumentTypeId { get; set; }
        [Required(ErrorMessage = "name_required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "code_required")]
        public string Code { get; set; }
        public string CodePath { get; set; }
    }
}