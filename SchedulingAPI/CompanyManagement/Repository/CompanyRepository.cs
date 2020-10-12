using CompanyManagement.Models;
using Entity.Base;
using SQLContext;
using System.Threading.Tasks;

namespace CompanyManagement.Repository
{
    public class CompanyRepository : RepositoryBase, ICompanyRepository
    {
        public CompanyRepository(string connectionString) : base(connectionString) { }

        public async Task<ResponseBase<Company>> Save(Company company, long userId) =>
            await ExecuteScalarAsync<Company>(
                "[dbo].[Company_Save]",
                new
                {
                    company.Name,
                    company.FileId,
                    userId
                }

            );

        public async Task<ResponseBase<Company>> SelectCompany(long userId) =>
            await QueryMultipleAsync(
                "[dbo].[Company_Select]",
                new { userId },
                (reader) => reader.ReadFirst<Company>()
            );
    }
}
