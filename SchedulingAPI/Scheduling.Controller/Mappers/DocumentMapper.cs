using Common.Extensions;
using Scheduling.Contract.Models;
using Scheduling.Controller.Requests;
using Scheduling.Controller.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Controller.Mappers
{
    public static class DocumentMapper
    {
        public static Document MapModel(DocumentRequest request) =>
            new Document()
            {
                DocumentId = request.DocumentId,
                DocumentStatus = new DocumentStatus() { DocumentStatusId = request.DocumentStatusId },
                DocumentType = new DocumentType() { CodePath = request.DocumentTypeCodePath },
                Date = request.Date,
                IssuingPlace = request.IssuingPlace,
                DateTo = request.DateTo,
                DateFrom = request.DateFrom,
                Note = request.Note,
                PricelistType = new PricelistType() { PricelistTypeId = request.PricelistTypeId },
                Change = request.Change,
                Paid = request.Paid,
                Schedule = new Schedule() { ScheduleId = request.ScheduleId },
                Customer = new Customer() { CustomerId = request.CustomerId },
                OrganizationUnits = new List<OrganizationUnit>(request.OrganizationUnitIds.Select(org => new OrganizationUnit() { OrganizationUnitId = org })),
                DocumentDetails = request.DocumentDetails.Select(DocumentDetailMapper.MapModel)
            };

        public static DocumentView MapView(Document model) =>
            new DocumentView()
            {
                DocumentId = model.DocumentId,
                DocumentStatus = new DocumentStatusView() { Code = model.DocumentStatus.Code },
                FullNumber = model.FullNumber,
                Date = model.Date,
                IssuingPlace = model.IssuingPlace,
                DateTo = model.DateTo,
                DateFrom = model.DateFrom,
                Note = model.Note,
                PricelistType = new PricelistTypeView() { Name = model.PricelistType.Name },
                Change = model.Change,
                Paid = model.Paid,
                Schedule = model.Schedule != null ? new ScheduleView() { ScheduleId = model.Schedule.ScheduleId } : null,
                CustomerName = model.CustomerName,
                OrganizationUnitNames = model.OrganizationUnitNames,
                DocumentDetails = model.DocumentDetails != null ? model.DocumentDetails.Select(DocumentDetailMapper.MapView) : null
            };

        public static IEnumerable<DocumentView> MapView(IEnumerable<Document> model) =>
            model.Select(c => MapView(c));
    }
}
