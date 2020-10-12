using CodebookManagement.Models;
using SQLContext.Attributes;
using SQLContext.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduling.Contract.Models
{
    public class DocumentType : ICodebook
    {
        [PrimaryKey]
        public int DocumentTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CodePath { get; set; }
        [Join(JoinType.Inner, "DocumentTypeId", "DocumentTypeId")]
        public DocumentTypeCompany DocumentTypeCompany { get; set; }
        public int Id => DocumentTypeId;

        public DocumentType() 
        {
            DocumentTypeCompany = new DocumentTypeCompany();
        }
    }
    public class DocumentTypeCompany
    {
        [PrimaryKey]
        public int DocumentTypeCompanyId { get; set; }
        public Guid CompanyId { get; set; }
        public int DocumentTypeId { get; set; }
        public int Year { get; set; }
        public int DefaultNumber { get; set; }
        [Join(JoinType.Inner, "CompanyId", "CompanyId")]
        public User User { get; set; } = new User();
    }

}