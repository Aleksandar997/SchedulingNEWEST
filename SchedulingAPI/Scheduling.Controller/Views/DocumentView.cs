using Scheduling.Contract.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Scheduling.Controller.Views
{
    public class DocumentView
    {
        public long DocumentId { get; set; }
        public string DocumentTypeCodePath { get; set; }
        public DocumentStatusView DocumentStatus { get; set; }
        public string FullNumber { get; set; }
        public DateTime? Date { get; set; }
        public string IssuingPlace { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DateFrom { get; set; }
        public string Note { get; set; }
        public decimal? Sum { get; set; }
        public decimal? Paid { get; set; }
        public PricelistTypeView PricelistType { get; set; }
        public int? CustomerId { get; set; }

        public decimal? Change { get; set; }
        public ScheduleView Schedule { get; set; }
        public IEnumerable<DocumentDetailView> DocumentDetails { get; set; }

        public string OrganizationUnitNames { get; set; }
        public string CustomerName { get; set; }
    }
}
