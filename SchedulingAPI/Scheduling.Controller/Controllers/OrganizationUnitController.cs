using Entity.Base;
using Localization.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Contract.Models;
using Scheduling.Contract.Repository;
using Scheduling.Controller.Mappers;
using Scheduling.Controller.Requests;
using System.Threading.Tasks;
using Web.Adapters;
using Web.Extensions;

namespace Scheduling.Controller.Controllers
{
    [Route("api/organizationUnit")]
    public class OrganizationUnitController : ControllerAdapter
    {
        IOrganizationUnitRepository _organizationUnitRepository;
        public OrganizationUnitController(ILocalizationService localization, IOrganizationUnitRepository organizationUnitRepository) : base(localization)
        {
            _organizationUnitRepository = organizationUnitRepository;
        }
        [Authorize(Roles = "OrganizationUnit.View")]
        [HttpGet("selectAll")]
        public async Task<IActionResult> SelectAll(OrganizationUnitPaging paging) =>
            await _organizationUnitRepository.SelectAll(paging, UserId)
                                             .Then(OrganizationUnitMapper.MapView)
                                             .ToAutoResponse();

        [Authorize(Roles = "OrganizationUnit.View")]
        [HttpGet("selectById/{id}")]
        public async Task<IActionResult> SelectById(int id) =>
            await _organizationUnitRepository.SelectById(id, UserId)
                                             .Then(OrganizationUnitMapper.MapView)
                                             .ToAutoResponse();

        [Authorize(Roles = "OrganizationUnit.Save")]
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] OrganizationUnitRequest request) =>
            await ModelState.SkipChildValidation(request)
                            .OnValidation(() => _organizationUnitRepository.Save(OrganizationUnitMapper.MapModel(request), UserId)
                            .ToAutoResponse());

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id) =>
            await _organizationUnitRepository.Delete(id, UserId)
                                             .ToAutoResponse();
    }
}
