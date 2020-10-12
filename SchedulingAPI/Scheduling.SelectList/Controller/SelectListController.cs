using Localization.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduling.SelectList.Repository;
using System.Threading.Tasks;
using Web.Adapters;
using Web.Extensions;

namespace Scheduling.SelectList.Controller
{
    [Route("api/selectlist")]
    public class SelectListController : ControllerAdapter
    {
        ISelectListRepository _systemRepository;
        public SelectListController(ILocalizationService localization, ISelectListRepository systemRepository) : base(localization)
        {
            _systemRepository = systemRepository;
        }

        [Authorize(Roles = "Employee.SelectList")]
        [HttpGet("selectEmployees")]
        public async Task<IActionResult> SelectEmployees() =>
            await _systemRepository.SelectEmployees(UserId)
                                   .ToAutoResponse();

        [Authorize(Roles = "Product.SelectList")]
        [HttpGet("selectProducts")]
        public async Task<IActionResult> SelectProducts() =>
            await _systemRepository.SelectProducts(UserId)
                                   .ToAutoResponse();

        [Authorize(Roles = "ProductType.SelectList")]
        [HttpGet("selectProductTypes")]
        public async Task<IActionResult> SelectProductTypes() =>
            await _systemRepository.SelectProductTypes(UserId)
                                   .ToAutoResponse();

        [Authorize(Roles = "Product.SelectList")]
        [HttpGet("selectOrganizationUnits")]
        public async Task<IActionResult> SelectOrganizationUnits() =>
            await _systemRepository.SelectOrganizationUnits(UserId)
                                   .ToAutoResponse();

        [Authorize(Roles = "PricelistsType.SelectList")]
        [HttpGet("selectPricelistsTypes")]
        public async Task<IActionResult> SelectPricelistsTypes() =>
            await _systemRepository.SelectPricelistTypes()
                                   .ToAutoResponse();

        [Authorize(Roles = "DocumentStatus.SelectList")]
        [HttpGet("selectDocumentStatuses")]
        public async Task<IActionResult> SelectDocumentStatuses() =>
            await _systemRepository.SelectDocumentStatuses()
                                   .ToAutoResponse();

        [Authorize(Roles = "Role.SelectList")]
        [HttpGet("selectRoles")]
        public async Task<IActionResult> SelectRoles() =>
            await _systemRepository.SelectRoles(UserId)
                                   .ToAutoResponse();

    }
}
