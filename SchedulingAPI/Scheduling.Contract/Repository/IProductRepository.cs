using Entity.Base;
using Scheduling.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduling.Contract.Repository
{
    public interface IProductRepository
    {
        Task<ResponseBase<IEnumerable<Product>>> SelectAll(ProductPaging productPaging, long userId);
        Task<ResponseBase<Product>> SelectById(long productId);
        Task<ResponseBase<long>> Save(Product product, long userId);
        Task<ResponseBase<long>> Delete(long productId);
    }
}
