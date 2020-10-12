using Entity.Base;
using Scheduling.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduling.Contract.Repository
{
    public interface IOrganizationUnitRepository
    {
        Task<ResponseBase<IEnumerable<OrganizationUnit>>> SelectAll(OrganizationUnitPaging organizationUnitPaging, long userId);
        Task<ResponseBase<OrganizationUnit>> SelectById(int? organizationUnitId, long userId);
        Task<ResponseBase<int>> Save(OrganizationUnit organizationUnit, long userId);
        Task<ResponseBase<int>> Delete(int? organizationUnitId, long userId);
    }
}
