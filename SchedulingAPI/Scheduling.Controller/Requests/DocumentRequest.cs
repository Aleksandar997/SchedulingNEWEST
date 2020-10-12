using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Attributes;

namespace Scheduling.Controller.Requests
{
    public class DocumentRequest
    {
        public long DocumentId { get; set; }

        [Required(ErrorMessage = "document_type_required")]
        public string DocumentTypeCodePath { get; set; }

        [Required(ErrorMessage = "document_status_required")]
        public int? DocumentStatusId { get; set; }

        [Required(ErrorMessage = "date_required")]
        public DateTime? Date { get; set; }

        public string IssuingPlace { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DateFrom { get; set; }
        public string Note { get; set; }
        public decimal? Sum { get; set; }
        public decimal? Paid { get; set; }
        [ConditionalRequired("DocumentTypeCodePath;pricelists", "pricelist_type_required")]
        public int? PricelistTypeId { get; set; }

        [ConditionalRequired("DocumentTypeCodePath;receipts", "customer_required")]
        public int? CustomerId { get; set; }

        public decimal? Change { get; set; }
        public long? ScheduleId { get; set; }
        public ScheduleRequest Schedule { get; set; }
        [ChildValidation(
                "DocumentTypeCodePath", "pricelists", new string[] { "ProductId", "Price"}, ValidationType.IsZeroOrNull,
                "DocumentTypeCodePath", "receipts", new string[] { "ProductId", "Price", "EmployeeId" }, ValidationType.IsZeroOrNull
        )]
        public IEnumerable<DocumentDetailRequest> DocumentDetails { get; set; }

        [Required(ErrorMessage = "organization_unit_required")]
        public IEnumerable<int> OrganizationUnitIds { get; set; }
    }
}
