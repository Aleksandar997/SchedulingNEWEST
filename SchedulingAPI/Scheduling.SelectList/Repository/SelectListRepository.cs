using Entity.Base;
using Scheduling.SelectList.Models;
using SQLContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduling.SelectList.Repository
{
    public class SelectListRepository : RepositoryBase, ISelectListRepository
    {
        public SelectListRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<ResponseBase<IEnumerable<DocumentStatus>>> SelectDocumentStatuses() =>
            await QueryMultipleAsync(
                "[dbo].[DocumentStatus_SelectList]",
                (reader) => reader.Read<DocumentStatus>()
            );


        public async Task<ResponseBase<IEnumerable<Employee>>> SelectEmployees(long userId) =>
            await QueryMultipleAsync(
                "[dbo].[Employee_SelectList]",
                new { userId },
                (reader) => reader.Read<Employee>()
            );

        public async Task<ResponseBase<IEnumerable<OrganizationUnit>>> SelectOrganizationUnits(long userId) =>
            await QueryMultipleAsync(
                "[dbo].[OrganizationUnit_SelectList]",
                new { userId },
                (reader) => reader.Read<OrganizationUnit>()
            );

        public async Task<ResponseBase<IEnumerable<PricelistType>>> SelectPricelistTypes() =>
            await QueryMultipleAsync(
                "[dbo].[PricelistType_SelectList]",
                (reader) => reader.Read<PricelistType>()
            );

        public async Task<ResponseBase<IEnumerable<Product>>> SelectProducts(long userId) =>
            await QueryMultipleAsync(
                "[dbo].[Product_SelectList]",
                new { userId },
                (reader) => reader.Read<Product, ProductModel, Product>((productSelectList, product) =>
                {
                    productSelectList.model = product;
                    return productSelectList;
                }, splitOn: "ProductId")
            );

        public async Task<ResponseBase<IEnumerable<ProductType>>> SelectProductTypes(long userId) =>
            await QueryMultipleAsync(
                "[dbo].[ProductType_SelectList]",
                new { userId },
                (reader) => reader.Read<ProductType>()
            );

        public async Task<ResponseBase<IEnumerable<Role>>> SelectRoles(long userId) =>
            await QueryMultipleAsync(
                "[dbo].[Role_SelectList]",
                new { userId },
                (reader) => reader.Read<Role>()
            );
    }
}
