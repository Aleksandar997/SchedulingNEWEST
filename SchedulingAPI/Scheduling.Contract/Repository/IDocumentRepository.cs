using Entity.Base;
using Scheduling.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduling.Contract.Repository
{
    public interface IDocumentRepository
    {
        Task<ResponseBase<IEnumerable<Document>>> SelectAll(DocumentPaging documentPaging, long userId);
        Task<ResponseBase<Document>> SelectById(long documentId, long userId);
        Task<ResponseBase<long>> Save(Document document, long userId);
        Task<ResponseBase<long>> Delete(long documentId);
    }
}
