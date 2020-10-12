using Entity.Base;
using Scheduling.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace Scheduling.Contract.Repository
{
    public interface IChartRepository
    {
        //Task<ResponseBase<IEnumerable<BarChart>>> SelectMostSoldProductsAndServices(int userId);
        Task<ResponseBase<int>> SetDragPosition(ChartMetaData chartMetaData, long userId);
        //Task<ResponseBase<IEnumerable<BarChart>>> SelectOrganizationUnitBySales(int userId);
        Task<ResponseBase<IEnumerable<Chart>>> SelectChartData(long userId, string procedureName);
        Task<ResponseBase<IEnumerable<ChartGrouped>>> SelectChartGroupedData(long userId, string procedureName);
    }
}
