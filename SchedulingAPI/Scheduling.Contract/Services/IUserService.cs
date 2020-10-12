using Entity.Base;
using Scheduling.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchedulingAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseBase<IEnumerable<User>>> SelectAll(UserPaging paging, long userId);
        Task<ResponseBase<User>> Save(User user, long userId);
        Task<ResponseBase<User>> SelectById(long userId, long sysUserId);
        Task<ResponseBase<int>> Delete(long userId, long sysUserId);
    }
}
