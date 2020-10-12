using Common.Extensions;
using Localization.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Contract.Models;
using SchedulingAPI.Services.Interfaces;
using System.Threading.Tasks;
using Web.Adapters;
using Web.Extensions;

namespace Scheduling.Controller.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerAdapter
    {
        IUserService _userService;
        public UserController(ILocalizationService localization, IUserService userService) : base(localization)
        {
            _userService = userService;
        }

        [Authorize(Roles = "User.View")]
        [HttpGet("selectAll")]
        public async Task<IActionResult> SelectAll(UserPaging paging) =>
            await _userService.SelectAll(paging, UserId).ToAutoResponse();

        [Authorize(Roles = "User.Save")]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] User user) =>
            await _userService.Save(user, UserId).ToAutoResponse();

        [Authorize(Roles = "User.View")]
        [HttpGet("selectById")]
        public async Task<IActionResult> SelectById() =>
            await _userService.SelectById(HttpContext.Request.Query.ToObject<int>(), UserId).ToAutoResponse();

        [Authorize(Roles = "User.Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            await _userService.Delete(id, UserId).ToAutoResponse();
    }
}
