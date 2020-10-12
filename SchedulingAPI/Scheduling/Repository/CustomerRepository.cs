using Entity.Base;
using Scheduling.Contract.Models;
using Scheduling.Contract.Repository;
using SQLContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduling.Repository
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString) { }
        public async Task<ResponseBase<Customer>> Save(Customer customer, long userId) =>
            await ExecuteScalarAsync<Customer>(
                "[dbo].[Customer_Save]",
                 new
                 {
                     customer.CustomerId,
                     customer.FirstName,
                     customer.LastName,
                     customer.PhoneNumber,
                     userId
                 }
            );

        public async Task<ResponseBase<IEnumerable<Customer>>> SelectAll(long userId) =>
            await QueryMultipleAsync(
                "[dbo].[Customer_SelectAll]",
                 new { userId },
                (reader) => reader.Read<Customer>()
            );

        public async Task<ResponseBase<Customer>> SelectById(int customerId) =>
            await QueryMultipleAsync(
                "[dbo].[Customer_SelectById]",
                 new { customerId },
                (reader) => reader.ReadFirst<Customer>()
            );
    }
}
