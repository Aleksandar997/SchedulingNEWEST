using System.ComponentModel.DataAnnotations;
using Web.Attributes;

namespace Scheduling.Controller.Requests
{
    public class DocumentDetailRequest
    {
        public long? DocumentDetailId { get; set; }
        public int? DocumentId { get; set; }

        [Required(ErrorMessage = "product_required")]
        public long? ProductId { get; set; }

        [Required(ErrorMessage = "quantity_required")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "discount_required")]
        public decimal? Discount { get; set; }

        [Required(ErrorMessage = "price_with_discount_required")]
        public decimal? PriceWithDiscount { get; set; }

        [Required(ErrorMessage = "price_required")]
        public decimal? Price { get; set; }

        public int? EmployeeId { get; set; }
    }
}
