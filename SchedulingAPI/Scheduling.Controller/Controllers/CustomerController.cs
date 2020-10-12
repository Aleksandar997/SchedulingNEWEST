using Entity.Base;
using Localization.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Contract.Repository;
using Scheduling.Controller.Mappers;
using Scheduling.Controller.Requests;
using System.Threading.Tasks;
using Web.Adapters;
using Web.Extensions;

namespace Scheduling.Controller.Controllers
{
    [Route("api/customer")]
    public class CustomerController : ControllerAdapter
    {
        ICustomerRepository _customerRepository;
        public CustomerController(ILocalizationService localization, ICustomerRepository customerRepository) : base(localization)
        {
            _customerRepository = customerRepository;
        }

        [Authorize(Roles = "Customer.Save")]
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] CustomerRequest request) =>
            await ModelState.SkipChildValidation(request)
                            .OnValidation(() => _customerRepository.Save(CustomerMapper.MapModel(request), UserId)
                            .Then(CustomerMapper.MapView)
                            .ToAutoResponse());

        [Authorize(Roles = "Customer.View")]
        [HttpGet("selectAll")]
        public async Task<IActionResult> SelectAll() =>
            await _customerRepository.SelectAll(UserId)
                                     .Then(CustomerMapper.MapView)
                                     .ToAutoResponse();

        [Authorize(Roles = "Customer.View")]
        [HttpGet("selectbyId/{id}")]
        public async Task<IActionResult> SelectbyId(int id) =>
            await _customerRepository.SelectById(id)
                                     .Then(CustomerMapper.MapView)
                                     .ToAutoResponse();
    }
}
