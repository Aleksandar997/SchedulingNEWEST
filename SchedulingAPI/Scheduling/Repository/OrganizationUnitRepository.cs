using Entity.Base;
using Scheduling.Contract.Models;
using Scheduling.Contract.Repository;
using SQLContext;
using SQLContext.Factories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduling.Repository
{
    public class OrganizationUnitRepository : RepositoryBase, IOrganizationUnitRepository
    {
        public OrganizationUnitRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<ResponseBase<IEnumerable<OrganizationUnit>>> SelectAll(OrganizationUnitPaging organizationUnitPaging, long userId) =>
            await QueryMultipleAsync(
                "[dbo].[OrganizationUnit_SelectAll]",
                new
                {
                    organizationUnitPaging.SortBy,
                    organizationUnitPaging.SortOrder,
                    organizationUnitPaging.Skip,
                    organizationUnitPaging.Take,
                    organizationUnitPaging.Name,
                    organizationUnitPaging.Code,
                    userId
                },
                (reader) => reader.Read<OrganizationUnit>()
            );


        public async Task<ResponseBase<OrganizationUnit>> SelectById(int? organizationUnitId, long userId) =>
            await QueryMultipleAsync(
                "[dbo].[OrganizationUnit_SelectById]",
                 new { organizationUnitId, userId },
                (reader) => reader.ReadFirst<OrganizationUnit>()
            );

        public async Task<ResponseBase<int>> Save(OrganizationUnit organizationUnit, long userId) =>
            await ExecuteScalarAsync<int>(
                "[dbo].[OrganizationUnit_Save]",
                new
                {
                    organizationUnit.OrganizationUnitId,
                    organizationUnit.Name,
                    organizationUnit.Code,
                    organizationUnit.Active,
                    organizationUnit.BindScheduleToEmployee,
                    userId
                }
            );

        public async Task<ResponseBase<int>> Delete(int? organizationUnitId, long userId) =>
            await ExecuteScalarAsync<int>(
                "[dbo].[OrganizationUnit_Delete]",
                new { organizationUnitId, userId }
            );
    }
}
