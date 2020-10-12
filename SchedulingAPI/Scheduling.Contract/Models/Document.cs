using Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling.Contract.Models
{
    public class Document
    {
        public long DocumentId { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }
        public int DocumentStatusId { get; set; }
        public DocumentStatus DocumentStatus { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string FullNumber { get; set; }
        public DateTime? Date { get; set; }
        public string IssuingPlace { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DateFrom { get; set; }
        public string Note { get; set; }
        private decimal? sum;

        public decimal? Sum 
        {
            get => (sum ?? DocumentDetails.Sum(x => x.PriceWithDiscount));
            set => sum = value;
        }


        //public List<int> GetEmployees() => DocumentDetails.Where(x => x.EmployeeId.HasValue).Select(x => x.EmployeeId.Value).Distinct().ToList();

        public decimal? Paid { get; set; }
        public int PricelistTypeId { get; set; }
        public PricelistType PricelistType { get; set; }
        public decimal? Change { get; set; }
        public long ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public IEnumerable<DocumentDetail> DocumentDetails { get; set; }
        public List<OrganizationUnit> OrganizationUnits { get; set; }
        public string OrganizationUnitNames { get; set; }
        public string CustomerName { get; set; }
    }
    public class DocumentPaging : BasePaging
    {
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public List<long> OrganizationUnits = new List<long>();
        public List<long> Customers = new List<long>();
        public List<int> PricelistTypes = new List<int>();
        public DateTime? Date { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DateFrom { get; set; }
        public int? DocumentStatusId { get; set; }
    }
}
