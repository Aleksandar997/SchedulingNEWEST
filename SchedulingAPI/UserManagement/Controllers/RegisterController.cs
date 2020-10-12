using Localization.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Service.Interfaces;
using Web.Adapters;
using Web.Extensions;

namespace UserManagement.Controllers
{
    [Route("api/register")]
    public class RegisterController : ControllerAdapter
    {
        IRegisterService _registerService;
        public RegisterController(ILocalizationService localization, IRegisterService registerService) : base(localization)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] RegisterModel request) =>
            await ModelState.SkipChildValidation(request)
                            .OnValidation(() => _registerService.Registration(request)
                            .ToAutoResponse());
    }
}
