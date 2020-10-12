namespace Scheduling.Contract.Models
{
    public class PricelistType
    {
        public int? PricelistTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}