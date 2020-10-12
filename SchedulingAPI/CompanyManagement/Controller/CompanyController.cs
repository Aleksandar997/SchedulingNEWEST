using CompanyManagement.Models;
using CompanyManagement.Repository;
using Entity.Base;
using Localization.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Adapters;
using Web.Extensions;

namespace CompanyManagement.Controller
{

    [Route("api/framework/company")]
    public class CompanyController : ControllerAdapter
    {
        ICompanyRepository _companyRepository;
        public CompanyController(ILocalizationService localization, ICompanyRepository companyRepository) : base(localization)
        {
            _companyRepository = companyRepository;
        }

        //[Authorize(Roles = "Customer.Save")]
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] Company request) =>
            await ModelState.SkipChildValidation(request)
                            .OnValidation(() => _companyRepository.Save(request, UserId).ToAutoResponse());

        //[Authorize(Roles = "Customer.View")]
        [HttpGet("getCompany")]
        public async Task<IActionResult> SelectCustomers() =>
            await ModelState.OnValidation(() => _companyRepository.SelectCompany(UserId).ToAutoResponse());
    }
 
}
