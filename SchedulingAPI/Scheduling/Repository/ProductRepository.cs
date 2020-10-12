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
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<ResponseBase<IEnumerable<Product>>> SelectAll(ProductPaging productPaging, long userId) =>
            await QueryMultipleAsync(
                "[dbo].[Product_SelectAll]",
                 new
                 {
                     productPaging.SortBy,
                     productPaging.SortOrder,
                     productPaging.Skip,
                     productPaging.Take,
                     productPaging.Name,
                     productPaging.Code,
                     ProductTypes = ParameterHelper.ToUserDefinedTableType(productPaging.ProductTypes.Select(x => new { value = x }), "IntList"),
                     OrganizationUnits = ParameterHelper.ToUserDefinedTableType(productPaging.OrganizationUnits.Select(x => new { value = x }), "IntList"),
                     userId
                 },
                (reader) =>
                {
                    var res = reader.Read<Product, ProductType, Product>((product, productType) =>
                    {
                        product.ProductType = productType;
                        return product;
                    }, splitOn: "ProductTypeId");
                    var count = reader.ReadFirstOrDefault<int>();
                    return (res, count);
                }
            );

        public async Task<ResponseBase<Product>> SelectById(long productId) =>
            await QueryMultipleAsync(
                "[dbo].[Product_SelectById]",
                 new { productId },
                (reader) =>
                {
                    var product = reader.ReadFirstOrDefault<Product>() ?? new Product();
                    //product.OrganizationUnits = read.Read.Read<long>().ToList();
                    //product.ProductPricelist = read.Read.Read<ProductPricelist>().ToList();
                    return product;
                }
            );

        public async Task<ResponseBase<long>> Save(Product product, long userId) =>
            await ExecuteScalarAsync<long>(
                "[dbo].[Product_Save]",
                new
                {
                    product.ProductId,
                    product.Name,
                    product.Code,
                    product.ProductTypeId,
                    product.Active,
                    //Pricelist = ParameterHelper.ToUserDefinedTableType(product.ProductPricelist.Select(x => 
                    //    new
                    //    {
                    //        x.OrganizationUnitId,
                    //        x.DocumentId,
                    //        x.DocumentDetailId,
                    //        x.Price
                    //    }
                    //).ToList(), "organization_unit_price"),
                    userId
                }

            );

        public async Task<ResponseBase<long>> Delete(long productId) =>
            await ExecuteScalarAsync<long>(
                "[dbo].[Product_Delete]",
                new { productId }
            );
    }
}
