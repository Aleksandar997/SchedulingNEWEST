using Entity.Base;
using Scheduling.Contract.Models;
using Scheduling.Contract.Repository;
using SQLContext;
using SQLContext.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.Repository
{
    public class ScheduleRepository : RepositoryBase, IScheduleRepository
    {
        public ScheduleRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<ResponseBase<long>> Delete(long scheduleId) =>
            await ExecuteScalarAsync<long>(
                "[dbo].[Schedule_DeleteById]",
                new { scheduleId }
            );

        public async Task<ResponseBase<long>> Save(Document document, long userId) =>
            await ExecuteScalarAsync<long>(
                "[dbo].[Schedule_Save]",
                new
                {
                    document.Schedule.ScheduleId,
                    document.Schedule.CustomerId,
                    //document.Schedule.PhoneNumber,
                    document.Schedule.Date,
                    document.Note,
                    document.Sum,
                    document.OrganizationUnits.FirstOrDefault().OrganizationUnitId,
                    userId,
                    Details = ParameterHelper.ToUserDefinedTableType(document.DocumentDetails.Select(x => new
                    {
                        x.DocumentDetailId,
                        x.Employee.EmployeeId,
                        x.Product.ProductId,
                        x.Price,
                        x.Quantity,
                        x.Discount,
                        x.PriceWithDiscount
                    }), "document_detail_list"),
                }

            );

        public async Task<ResponseBase<Document>> SelectById(int id, long userId) =>
            await QueryMultipleAsync(
                "[dbo].[Schedule_SelectById]",
                 new { ScheduleId = id, UserId = userId },
                (reader) =>
                {
                    var res = reader.Read<Document, Schedule, Document>((document, schedule) =>
                    {
                        //document.ScheduleId = schedule.ScheduleId;
                        document.Schedule = schedule;
                        return document;
                    }, splitOn: "ScheduleId").FirstOrDefault();
                    res.DocumentDetails = reader.Read<DocumentDetail>();
                    return res;
                }
            );


        public async Task<ResponseBase<IEnumerable<ScheduleGroup>>> SelectAll(SchedulePaging paging, long userId) =>
            await QueryMultipleAsync(
                "[dbo].[Schedule_SelectAllInMonth]",
                 new
                 {
                     paging.DateFrom,
                     paging.DateTo,
                     Employees = ParameterHelper.ToUserDefinedTableType(paging.Employees.Select(x => new { value = x }), "IntList"),
                     OrganizationUnits = ParameterHelper.ToUserDefinedTableType(paging.OrganizationUnits.Select(x => new { value = x }), "IntList"),
                     paging.ScheduleGroupId,
                     userId
                 },
                (reader) =>
                {
                    var groups = reader.Read<ScheduleGroup>();
                    var schedules = reader.Read<Schedule, Customer, Schedule>((schedule, customer) =>
                    {
                        schedule.Customer = customer;
                        return schedule;
                    }, splitOn: "CustomerId");
                    groups.FirstOrDefault(x => x.Selected).Schedules = schedules;
                    return groups;
                }
            );
    }
}
