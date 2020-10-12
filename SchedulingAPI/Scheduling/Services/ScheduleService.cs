using Entity.Base;
using Microsoft.AspNetCore.SignalR;
using Scheduling.Contract.Models;
using Scheduling.Contract.Repository;
using Scheduling.Hubs;
using SchedulingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Hangfire;

namespace Scheduling.Services
{
    public class ScheduleService : IScheduleService
    {
        IScheduleRepository _scheduleRepository;
        IHubContext<SignalHub> _hubContext;
        public ScheduleService(IScheduleRepository scheduleRepository, IHubContext<SignalHub> hubContext)
        {
            _scheduleRepository = scheduleRepository;
            _hubContext = hubContext;
        }

        public async Task<ResponseBase<long>> Delete(long scheduleId) =>
            await _scheduleRepository.Delete(scheduleId);
        public async Task<ResponseBase<long>> Save(Document document, long userId, int EmployeeId)
        {
            var res = await _scheduleRepository.Save(document, userId);
            if (res.Status == ResponseStatus.Success)
            {
                //await NotificationHubManager.Notify(res.Data, document.GetEmployees().Where(x => x != EmployeeId).ToList());
                //var currentDate = DateTime.Now;
                //var b = document.Schedule.Date - currentDate;
                //var a = new DateTimeOffset(currentDate, TimeSpan.FromDays((document.Schedule.Date - currentDate).TotalDays));
                //if (document.Schedule.Date > currentDate)
                //    BackgroundJob.Schedule(() => Console.WriteLine("a"), new DateTimeOffset(currentDate, TimeSpan.FromDays((document.Schedule.Date - currentDate).TotalDays)));
            }
            //TimeSpan.FromDays((document.Schedule.Date - currentDate).TotalDays)
            return res;
        }

        public async Task<ResponseBase<Document>> SelectById(int id, long userId) =>
            await _scheduleRepository.SelectById(id, userId);


        public async Task<ResponseBase<IEnumerable<ScheduleGroup>>> SelectAll(SchedulePaging paging, long userId) =>
            await _scheduleRepository.SelectAll(paging, userId);
    }
}
