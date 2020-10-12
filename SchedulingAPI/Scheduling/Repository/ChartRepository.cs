using Entity.Base;
using Scheduling.Contract.Models;
using Scheduling.Contract.Repository;
using SQLContext;
using SQLContext.Factories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace Scheduling.Repository
{
    public class ChartRepository : RepositoryBase, IChartRepository
    {
        public ChartRepository(string connectionString) : base(connectionString) { }

        public async Task<ResponseBase<IEnumerable<Chart>>> SelectChartData(long userId, string procedureName) =>
            await QueryMultipleAsync(
                procedureName,
                 new { userId },
                (reader) => reader.Read<Chart>()
            );

        public async Task<ResponseBase<IEnumerable<ChartGrouped>>> SelectChartGroupedData(long userId, string procedureName) =>
            await QueryMultipleAsync(
                procedureName,
                 new { userId },
                (reader) => ChartGrouped.ToGroup(reader.Read<Chart>().ToList())
            );

        public async Task<ResponseBase<int>> SetDragPosition(ChartMetaData chartMetaData, long userId) =>
            await ExecuteScalarAsync<int>(
                "[dbo].[Chart_UpdatePosition]",
                new
                {
                    chartMetaData.Name,
                    chartMetaData.X,
                    chartMetaData.Y,
                    userId
                }
            );
    }
}


