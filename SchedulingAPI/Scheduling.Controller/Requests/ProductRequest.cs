using Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Web.Attributes;

namespace Scheduling.Controller.Requests
{
    public class ProductRequest
    {
        [PrimaryKey]
        public long ProductId { get; set; }
        [ChildValidation(new string[] { "ProductTypeId" })]
        public ProductTypeRequest ProductType { get; set; }
        [Required(ErrorMessage = "code_required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "name_required")]
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public List<long> OrganizationUnits { get; set; }
        public string OrganizationUnitsString { get; set; }
    }
}