using Entity.Base;
using Scheduling.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduling.Contract.Repository
{
    public interface ICustomerRepository
    {
        Task<ResponseBase<Customer>> Save(Customer customer, long userId);
        Task<ResponseBase<IEnumerable<Customer>>> SelectAll(long userId);
        Task<ResponseBase<Customer>> SelectById(int customerId);
    }
}
