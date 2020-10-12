using Common.Extensions;
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
    [Route("api/document")]
    public class DocumentController : ControllerAdapter
    {
        IDocumentRepository _documentRepository;
        public DocumentController(ILocalizationService localization, IDocumentRepository documentRepository) : base(localization)
        {
            _documentRepository = documentRepository;
        }

        [Authorize(Roles = "Document.View")]
        [HttpGet("selectAllByType")]
        public async Task<IActionResult> SelectAll() =>
            await _documentRepository.SelectAll(HttpContext.Request.Query.ToObject<DocumentPaging>(), UserId)
                                     //.Then(DocumentMapper.MapView)
                                     .ToAutoResponse();

        //[Authorize(Roles = "Document.View")]
        [HttpGet("selectById/{id}")]
        public async Task<IActionResult> SelectById(long id) =>
             await _documentRepository.SelectById(id, UserId)
                                      //.Then(DocumentMapper.MapView)
                                      .ToAutoResponse();

        [Authorize(Roles = "Document.Save")]
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] DocumentRequest request) =>
            await ModelState.SkipChildValidation(request)
                            .OnValidation(() => _documentRepository.Save(DocumentMapper.MapModel(request), UserId)
                            .ToAutoResponse());

        [Authorize(Roles = "Document.Delete")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id) =>
            await _documentRepository.Delete(id)
                                     .ToAutoResponse();
    }
}
