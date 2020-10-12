using CompanyManagement.Models;
using Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Repository
{
    public interface ICompanyRepository
    {
        Task<ResponseBase<Company>> Save(Company company, long userId);
        Task<ResponseBase<Company>> SelectCompany(long userId);
    }
}
