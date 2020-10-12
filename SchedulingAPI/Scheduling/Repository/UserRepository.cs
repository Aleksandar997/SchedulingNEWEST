using Common.Extensions;
using Entity.Base;
using Scheduling.Contract.Models;
using Scheduling.Contract.Repository;
using SQLContext;
using SQLContext.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using User = Scheduling.Contract.Models.User;

namespace Scheduling.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString) { }

        public async Task<ResponseBase<IEnumerable<User>>> SelectAll(UserPaging paging, long userId) =>
            await QueryMultipleAsync(
                "[dbo].[User_SelectAll]",
                 new
                 {
                     paging.SortBy,
                     paging.SortOrder,
                     paging.Skip,
                     paging.Take,
                     paging.FirstName,
                     paging.LastName,
                     userId
                 },
                (reader) =>
                {
                    var res = reader.Read<User, Employee, User>((user, employee) =>
                    {
                        user.Employee = employee;
                        return user;
                    }, splitOn: "EmployeeId");
                    var count = reader.ReadFirstOrDefault<int>();
                    return (res, count);
                }
            );

        public async Task<ResponseBase<User>> Save(User user, long userId) =>
            await ExecuteScalarAsync<User>(
                "[dbo].[User_Save]",
                new
                {
                    user.UserId,
                    user.Employee.EmployeeId,
                    user.FirstName,
                    user.LastName,
                    user.UserName,
                    user.Password,
                    user.Email,
                    user.Active,
                    Roles = ParameterHelper.ToUserDefinedTableType(user.Roles.Select(x => new { value = x.RoleId }), "IntList"),
                    OrganizationUnits = ParameterHelper.ToUserDefinedTableType(user.Employee.OrganizationUnits.IfNull().Select(x => new { value = x }), "IntList"),
                    Products = ParameterHelper.ToUserDefinedTableType(user.Employee.Products.IfNull().Select(x => new { value = x }), "IntList"),
                    user.Employee.IdentificationNumber,
                    sysUserId = userId
                }
            );


        public async Task<ResponseBase<User>> SelectById(long userId, long sysUserId) =>
            await QueryMultipleAsync(
                "[dbo].[User_SelectById]",
                 new { userId, sysUserId },
                (reader) =>
                {
                    var res = reader.Read<User, Employee, User>((user, employee) =>
                    {
                        user.Employee = employee;
                        return user;
                    }, splitOn: "EmployeeId").FirstOrDefault();
                    res.Roles = reader.Read<Role>().ToList();
                    if (res.Employee != null)
                    {
                        res.Employee.Products = reader.Read<long>();
                        res.Employee.OrganizationUnits = reader.Read<int>();
                    }
                    return res;
                }
            );

        public async Task<ResponseBase<int>> Delete(long userId, long sysUserId) =>
            await ExecuteScalarAsync<int>(
                "[dbo].[User_Delete]",
                new { userId, sysUserId }
            );
    }
}
