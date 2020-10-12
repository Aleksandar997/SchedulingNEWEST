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
    [Route("api/product")]
    public class ProductController : ControllerAdapter
    {
        IProductRepository _productRepository;
        public ProductController(ILocalizationService localization, IProductRepository productRepository) : base(localization)
        {
            _productRepository = productRepository;
        }

        [Authorize(Roles = "Product.View")]
        [HttpGet("selectAll")]
        public async Task<IActionResult> SelectAll(ProductPaging productPaging) =>
            await _productRepository.SelectAll(productPaging, UserId)
                                    .Then(ProductMapper.MapView)
                                    .ToAutoResponse();

        [Authorize(Roles = "Product.View")]
        [HttpGet("selectById/{id}")]
        public async Task<IActionResult> SelectById(int id) =>
            await _productRepository.SelectById(id)
                                    .Then(ProductMapper.MapView)
                                    .ToAutoResponse();

        [Authorize(Roles = "Product.Save")]
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] ProductRequest request) =>
            await ModelState.SkipChildValidation(request)
                            .OnValidation(() => _productRepository.Save(ProductMapper.MapModel(request), UserId)
                            .ToAutoResponse());

        [Authorize(Roles = "Product.Delete")]
        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id) =>
            await _productRepository.Delete(id)
                                    .ToAutoResponse();
    }
}
