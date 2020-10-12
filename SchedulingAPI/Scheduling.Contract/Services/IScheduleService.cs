using Entity.Base;
using Scheduling.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchedulingAPI.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<ResponseBase<Document>> SelectById(int id, long userId);
        public Task<ResponseBase<IEnumerable<ScheduleGroup>>> SelectAll(SchedulePaging paging, long userId);
        public Task<ResponseBase<long>> Save(Document document, long userId, int EmployeeId);
        public Task<ResponseBase<long>> Delete(long scheduleId);
    }
}
