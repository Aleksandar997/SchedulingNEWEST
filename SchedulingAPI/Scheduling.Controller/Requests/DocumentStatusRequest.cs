using System.ComponentModel.DataAnnotations;

namespace Scheduling.Controller.Requests
{
    public class DocumentStatusRequest
    {
        public int DocumentStatusId { get; set; }
        [Required(ErrorMessage = "code_required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "name_required")]
        public string Name { get; set; }
    }
}
