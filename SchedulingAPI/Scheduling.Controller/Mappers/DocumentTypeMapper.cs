using Scheduling.Contract.Models;
using Scheduling.Controller.Requests;
using Scheduling.Controller.Views;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling.Controller.Mappers
{
    public static class DocumentTypeMapper
    {
        public static DocumentType MapModel(DocumentTypeRequest request) =>
            new DocumentType()
            {
                DocumentTypeId = request.DocumentTypeId,
                Name = request.Name,
                Code = request.Code
            };

        public static DocumentTypeView MapView(DocumentType model) =>
            new DocumentTypeView()
            {
                DocumentTypeId = model.DocumentTypeId,
                Name = model.Name,
                Code = model.Code,
                CodePath = model.CodePath
            };

        public static IEnumerable<DocumentTypeView> MapView(IEnumerable<DocumentType> model) =>
            model.Select(c => MapView(c));
    }
}
