using Localization.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Contract.Repository;
using System.Threading.Tasks;
using UserManagement.Models;
using Web.Adapters;
using Web.Extensions;

namespace Scheduling.Controller.Controllers
{
    [Route("api/chart")]
    public class ChartController : ControllerAdapter
    {
        IChartRepository _chartRepository;
        public ChartController(ILocalizationService localization, IChartRepository chartRepository) : base(localization)
        {
            _chartRepository = chartRepository;
        }

        [Authorize]
        [HttpGet("mostSoldProductsAndServices/get")]
        public async Task<IActionResult> SelectMostSoldProductsAndServices() =>
            await _chartRepository.SelectChartData(UserId, "[dbo].[Chart_MostSoldProductsAndServices]").ToAutoResponse();

        [Authorize]
        [HttpPut("updatePosition")]
        public async Task<IActionResult> UpdatePosition([FromBody] ChartMetaData chartMetaData) =>
            await _chartRepository.SetDragPosition(chartMetaData, UserId).ToAutoResponse();


        [Authorize]
        [HttpGet("organizationUnitBySales/get")]
        public async Task<IActionResult> SelectOrganizationUnitBySales() =>
            await _chartRepository.SelectChartData(UserId, "[dbo].[Chart_OrganizationUnitBySales]").ToAutoResponse();


        [Authorize]
        [HttpGet("saleInLast12MonthsByProduct/get")]
        public async Task<IActionResult> SelectSaleInLast12MonthsByProduct() =>
            await _chartRepository.SelectChartGroupedData(UserId, "[dbo].[Chart_SaleInLast12MonthsByProduct]").ToAutoResponse();

        [Authorize]
        [HttpGet("averageSaleDuringDayByHours/get")]
        public async Task<IActionResult> SelectAverageSaleDuringDayByHours() =>
            await _chartRepository.SelectChartGroupedData(UserId, "[dbo].[Chart_AverageSaleDuringDayByHours]").ToAutoResponse();
    }

}
