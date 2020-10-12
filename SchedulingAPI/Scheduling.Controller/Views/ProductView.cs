using System.Collections.Generic;

namespace Scheduling.Controller.Views
{
    public class ProductView
    {
        public long? ProductId { get; set; }
        public int ProductTypeId { get; set; }
        public ProductTypeView ProductType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public long OrganizationUnitId { get; set; }
        //public List<ProductPricelist> ProductPricelist { get; set; }
        public List<long> OrganizationUnits { get; set; }
        public string OrganizationUnitsString { get; set; }
    }
    public class ProductSelectListView
    {
        public ProductView Product { get; set; }
        public decimal Price { get; set; }
        public int OrganizationUnitId { get; set; }
        public int EmployeeId { get; set; }
    }
}