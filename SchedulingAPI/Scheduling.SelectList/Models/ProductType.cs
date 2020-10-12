namespace Scheduling.SelectList.Models
{
    public class ProductType : ISelectListModel
    {
        public long Id => PricelistTypeId;
        public int PricelistTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
