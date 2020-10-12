using Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduling.Contract.Models
{
    public class Product
    {
        [PrimaryKey]
        public long? ProductId { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }

    }
    public class ProductPaging : BasePaging
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<int> OrganizationUnits = new List<int>();
        public List<int> ProductTypes = new List<int>();
    }

    public class ProductSelectList
    {
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int OrganizationUnitId { get; set; }
        public int EmployeeId { get; set; }
    }

    //public class ProductSelectList
    //{
    //    public IEnumerable<Product> Products { get; set; }
    //    public IEnumerable<ProductPricelist> ProductPricelist { get; set; }
    //    public ProductSelectList(IEnumerable<Product> products, IEnumerable<ProductPricelist> productPricelist)
    //    {
    //        Products = products;
    //        ProductPricelist = productPricelist;
    //    }
    //}
    //public class ProductPricelist
    //{
    //    public long? ProductId { get; set; }
    //    public string Name { get; set; }
    //    public long? DocumentId { get; set; }
    //    public int? OrganizationUnitId { get; set; }
    //    public string OrganizationUnitName { get; set; }
    //    public decimal? Price { get; set; }
    //    public long? DocumentDetailId { get; set; }
    //    public int EmployeeId { get; set; }
    //}

    //public class ProductSelectList
    //{
    //    public IEnumerable<Product> Products { get; set; }
    //    public IEnumerable<ProductPricelist> ProductPricelist { get; set; }
    //    public ProductSelectList(IEnumerable<Product> products, IEnumerable<ProductPricelist> productPricelist)
    //    {
    //        Products = products;
    //        ProductPricelist = productPricelist;
    //    }
    //}
}