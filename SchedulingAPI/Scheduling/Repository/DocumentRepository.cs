using Entity.Base;
using Scheduling.Contract.Models;
using Scheduling.Contract.Repository;
using SQLContext;
using SQLContext.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.Repository
{
    public class DocumentRepository : RepositoryBase, IDocumentRepository
    {
        public DocumentRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<ResponseBase<IEnumerable<Document>>> SelectAll(DocumentPaging documentPaging, long userId) =>
            await QueryMultipleAsync(
              "[dbo].[Document_SelectAll]",
              new
              {
                  documentPaging.SortBy,
                  documentPaging.SortOrder,
                  documentPaging.Skip,
                  documentPaging.Take,
                  documentPaging.DocumentType,
                  documentPaging.DocumentStatusId,
                  documentPaging.DocumentNumber,
                  OrganizationUnits = ParameterHelper.ToUserDefinedTableType(documentPaging.OrganizationUnits.Select(x => new { value = x }), "IntList"),
                  Customers = ParameterHelper.ToUserDefinedTableType(documentPaging.Customers.Select(x => new { value = x }), "IntList"),
                  PriceListTypes = ParameterHelper.ToUserDefinedTableType(documentPaging.PricelistTypes.Select(x => new { value = x }), "IntList"),
                  documentPaging.Date,
                  documentPaging.DateFrom,
                  documentPaging.DateTo,
                  userId
              },
              (reader) =>
              {
                  var res = reader.Read<Document, Schedule, PricelistType, DocumentStatus, Document>((document, schedule, priceListType, documentStatus) =>
                  {
                      document.PricelistType = priceListType;
                      document.DocumentStatus = documentStatus;
                      document.Schedule = schedule;
                      return document;
                  }, splitOn: "ScheduleId, PriceListTypeId, DocumentStatusId");
                  var count = reader.ReadFirstOrDefault<int>();
                  return (res, count);
              }
            );

        public async Task<ResponseBase<Document>> SelectById(long documentId, long userId) =>
            await QueryMultipleAsync(
                "[dbo].[Document_SelectById]",
                new { documentId, userId },
                (reader) =>
                {
                    var res = reader.Read<Document, Schedule, Document>((document, schedule) =>
                    {
                        document.Schedule = schedule;
                        return document;
                    }, splitOn: "ScheduleId").FirstOrDefault();
                    res.OrganizationUnits = reader.Read<OrganizationUnit>().ToList();
                    res.DocumentDetails = reader.Read<DocumentDetail>().ToList();
                    return res;
                }
            );

        public async Task<ResponseBase<long>> Save(Document document, long userId) =>
            await ExecuteScalarAsync<long>(
                "[dbo].[Document_Save]",
                new
                {
                    document.DocumentId,
                    DocumentType = document.DocumentType.CodePath,
                    document.DocumentStatus.DocumentStatusId,
                    document.Schedule.ScheduleId,
                    document.Date,
                    document.IssuingPlace,
                    document.DateFrom,
                    document.DateTo,
                    document.Note,
                    document.Sum,
                    document.PricelistType.PricelistTypeId,
                    DocumentDetails = ParameterHelper.ToUserDefinedTableType(document.DocumentDetails.Select(x => new
                    {
                        x.DocumentDetailId,
                        x.Employee.EmployeeId,
                        x.Product.ProductId,
                        x.Price,
                        x.Quantity,
                        x.Discount,
                        x.PriceWithDiscount
                    }), "document_detail_list"),
                    OrganizationUnits = ParameterHelper.ToUserDefinedTableType(document.OrganizationUnits.Select(x => new { value = x.OrganizationUnitId }), "IntList"),
                    userId
                }

            );

        public async Task<ResponseBase<long>> Delete(long documentId) =>
            await ExecuteScalarAsync<long>(
                "[dbo].[Document_Delete]",
                new { documentId }
            );
    }
}
