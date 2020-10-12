using Entity.Base;
using Scheduling.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduling.Contract.Repository
{
    public interface IScheduleRepository
    {
        public Task<ResponseBase<Document>> SelectById(int id, long userId);
        public Task<ResponseBase<IEnumerable<ScheduleGroup>>> SelectAll(SchedulePaging paging, long userId);
        public Task<ResponseBase<long>> Save(Document document, long userId);
        public Task<ResponseBase<long>> Delete(long scheduleId);
    }
}
