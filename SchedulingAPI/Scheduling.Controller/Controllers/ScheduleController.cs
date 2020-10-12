using Common.Extensions;
using Entity.Base;
using Localization.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Contract.Models;
using Scheduling.Controller.Mappers;
using Scheduling.Controller.Requests;
using SchedulingAPI.Services.Interfaces;
using System.Threading.Tasks;
using Web.Adapters;
using Web.Extensions;

namespace Scheduling.Controller.Controllers
{
    [Route("api/schedule")]
    public class ScheduleController : ControllerAdapter
    {
        IScheduleService _scheduleService;
        public ScheduleController(ILocalizationService localization, IScheduleService scheduleService) : base(localization)
        {
            _scheduleService = scheduleService;
        }
        //HttpContext.Request.Query.ToObject<SchedulePaging>()

        [Authorize(Roles = "Schedule.View")]
        [HttpGet("selectScheduleById/{id}")]
        public async Task<IActionResult> SelectById(int id) =>
            await _scheduleService.SelectById(id, UserId)
                                  .Then(DocumentMapper.MapView)
                                  .ToAutoResponse();

        [Authorize(Roles = "Schedule.View")]
        [HttpGet("selectSchedulesInMonth")]
        public async Task<IActionResult> SelectAll() =>
            await _scheduleService.SelectAll(HttpContext.Request.Query.ToObject<SchedulePaging>(), UserId)
                                  //.Then(ScheduleMapper.MapView)
                                  .ToAutoResponse();

        [Authorize(Roles = "Schedule.Save")]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] DocumentRequest request) =>
            await ModelState.SkipChildValidation(request)
                            .OnValidation(() => _scheduleService.Save(DocumentMapper.MapModel(request), UserId, EmployeeId)
                            .ToAutoResponse());

        [Authorize(Roles = "Schedule.Save")]
        [HttpDelete("scheduleDelete/{id}")]
        public async Task<IActionResult> Delete(int id) =>
            await _scheduleService.Delete(id)
                                  .ToAutoResponse();
    }
}
