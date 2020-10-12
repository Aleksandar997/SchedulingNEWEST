using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduling.Controller.Views
{
    public class DocumentDetailView
    {
        public long? DocumentDetailId { get; set; }
        public int? DocumentId { get; set; }
        public long? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Discount { get; set; }
        public decimal? PriceWithDiscount { get; set; }
        public decimal? Price { get; set; }

        public int? EmployeeId { get; set; }
    }
}
