using Localization.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Adapters;
using Web.Extensions;

namespace FileManagement.Controller
{
    [Route("api/file")]
    public class FileController : ControllerAdapter
    {
        //private IFileService _userService { get; set; }
        public FileController(ILocalizationService localization) : base(localization)
        {
        }

        [HttpPost("{previousFileName}")]
        public async Task<IActionResult> UploadFile(string previousFileName = null) =>
            await HttpContext.UploadFile(previousFileName).ToAutoResponse();
    }
}
