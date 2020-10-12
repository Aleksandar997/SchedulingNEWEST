namespace Scheduling.SelectList.Models
{
    public class Product : ISelectListModel
    {
        public long Id => model.ProductId;
        public string Name { get => model.Name; set { return; } }
        public ProductModel model { get; set; }
        public decimal Price { get; set; }
        public int OrganizationUnitId { get; set; }
        public int EmployeeId { get; set; }
        public string Code { get; set; }
    }

    public class ProductModel
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
    }
}
