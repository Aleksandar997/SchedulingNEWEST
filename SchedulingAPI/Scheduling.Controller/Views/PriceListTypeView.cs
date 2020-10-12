using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduling.Controller.Views
{
    public class PricelistTypeView
    {
        public int? PricelistTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}