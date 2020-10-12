using CodebookManagement.Models;
using CodebookManagement.Service;
using Localization.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Controller.Requests;
using System.Threading.Tasks;
using Web.Adapters;
using Web.Extensions;

namespace Scheduling.Controller.Controllers
{
    [Route("api/codebook")]
    public class CodebookController : ControllerAdapter
    {
        ICodebookService _codebookService;
        public CodebookController(ILocalizationService localization, ICodebookService codebookService) : base(localization)
        {
            _codebookService = codebookService;
        }

        //[Authorize(Roles = "DocumentType.View")]
        //[HttpGet("documentType/selectAll")]
        //public async Task<IActionResult> DocumentTypeSelectAll(CodebookPaging paging) =>
        //      await AutoResponse(() => _codebookService.SelectAll<DocumentType>
        //                           (
        //                                x => new { x.DocumentTypeId, x.Code, x.Name, x.DocumentTypeCompany.Year, 
        //                                            x.DocumentTypeCompany.DefaultNumber, x.DocumentTypeCompany.User.UserId },
        //                                x => x.DocumentTypeCompany.CompanyId == CompanyId
        //                           )
        //                         );

        //[Authorize(Roles = "ProductType.View")]
        //[HttpGet("productType/selectAll")]
        //public async Task<IActionResult> ProductTypeSelectAll(CodebookPaging paging) =>
        //        await AutoResponse(() => _codebookService.SelectAll<ProductType>(
        //                                x => new { x.ProductTypeId, x.Name, x.User.UserId },
        //                                x => x.CompanyId == CompanyId
        //                            ));


        //[Authorize(Roles = "DocumentType.Save")]
        //[HttpPost("documentType/save")]
        //public async Task<IActionResult> DocumentTypeSave([FromBody] DocumentType documentType)
        //{
        //    ModelState.RemoveChildValidation(documentType.DocumentTypeCompany);
        //    return await AutoResponse(() => _codebookService.Save(documentType, x => new { x.DocumentTypeCompany.DefaultNumber, x.DocumentTypeCompany.Year, x.DocumentTypeCompany.CompanyId  }));
        //}

        //[Authorize(Roles = "DocumentType.Save")]
        //[HttpPut("documentType/save/{id}")]
        //public async Task<IActionResult> DocumentTypeSave(int id, [FromBody] DocumentTypeRequest documentType)
        //{
        //    ModelState.RemoveChildValidation(documentType.DocumentTypeCompany);
        //    return await AutoResponse(() => _codebookService.Save(documentType, x => new { x.DocumentTypeCompany.DefaultNumber, x.DocumentTypeCompany.Year, x.DocumentTypeCompany.CompanyId }, id));
        //}

        //[Authorize(Roles = "ProductType.Save")]
        //[HttpPost("productType/save")]
        //public async Task<IActionResult> ProductTypeSave([FromBody] ProductType productType)
        //{
        //    ModelState.RemoveChildValidation(productType);
        //    return await AutoResponse(() => _codebookService.Save(productType, x => new { x. }));
        //}
    }
}
