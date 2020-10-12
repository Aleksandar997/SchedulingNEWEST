using Entity.Base;
using Scheduling.SelectList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduling.SelectList.Repository
{
    public interface ISelectListRepository
    {
        Task<ResponseBase<IEnumerable<Employee>>> SelectEmployees(long userId);
        Task<ResponseBase<IEnumerable<Product>>> SelectProducts(long userId);
        Task<ResponseBase<IEnumerable<ProductType>>> SelectProductTypes(long userId);
        Task<ResponseBase<IEnumerable<OrganizationUnit>>> SelectOrganizationUnits(long userId);
        Task<ResponseBase<IEnumerable<PricelistType>>> SelectPricelistTypes();
        Task<ResponseBase<IEnumerable<DocumentStatus>>> SelectDocumentStatuses();
        Task<ResponseBase<IEnumerable<Role>>> SelectRoles(long userId);
    }
}
