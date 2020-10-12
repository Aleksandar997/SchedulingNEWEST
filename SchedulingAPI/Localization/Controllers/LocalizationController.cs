using Localization.Models;
using Localization.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Localization.Controllers
{
    [Route("api/localization")]
    public class LocalizationController : Controller
    {
        ILocalizationService localization;
        public LocalizationController(ILocalizationService _localization)
        {
            localization = _localization;
        }

        [HttpPost("localizacionSelectAll")]
        public IActionResult LocalizacionSelectAll() => Ok(localization.GetAllLocalizationByCulture().ToList());

        [HttpGet("resourceSelectAll")]
        public async Task<IActionResult> ResourceSelectAll(LocalizationPaging localizationPaging) => Ok(await localization.SelectAll(localizationPaging));

        [HttpGet("resourceById/{resourceId}")]
        public async Task<IActionResult> SelectById(int resourceId) => Ok(await localization.SelectById(resourceId));

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody]Resource resource) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var UserId = Convert.ToInt32(User?.Claims.SingleOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value);
            return Ok(await localization.Save(resource, UserId));
        }

        [HttpGet("cultureSelectlist")]
        public async Task<IActionResult> CultureSelectlist() => Ok(await localization.CultureSelectlist());
    }
}
