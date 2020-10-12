using CodebookManagement.Models;
using SQLContext.Attributes;
using SQLContext.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduling.Contract.Models
{
    public class ProductType : ICodebook
    {
        [PrimaryKey]
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public int Id => ProductTypeId;
        public bool Active { get; set; }
        public string Code { get; set; }
        public Guid CompanyId { get; set; }
        [Join(JoinType.Inner, "CompanyId", "CompanyId")]
        public User User { get; set; }
    }
}