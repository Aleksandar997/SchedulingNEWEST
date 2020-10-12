using SchedulingAPI.Services.Interfaces;
using sysIUserService = UserManagement.Service.Interfaces.IUserService;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Base;
using Scheduling.Contract.Repository;
using Scheduling.Contract.Models;

namespace Scheduling.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        sysIUserService _sysUserService;
        public UserService(IUserRepository userRepository, sysIUserService sysUserService)
        {
            _userRepository = userRepository;
            _sysUserService = sysUserService;
        }

        public async Task<ResponseBase<int>> Delete(long userId, long sysUserId) => await _userRepository.Delete(userId, sysUserId);

        public async Task<ResponseBase<User>> Save(User user, long userId)
        {
            if (user.UserId == 0 || user.UserId == null)
            {
                user.Password = _sysUserService.GeneratePassword();
                //await _sysUserService.SendMail(new UserCredentials(user));
            }
            return await _userRepository.Save(user, userId);
        }

        public async Task<ResponseBase<IEnumerable<User>>> SelectAll(UserPaging paging, long userId) => await _userRepository.SelectAll(paging, userId);

        public async Task<ResponseBase<User>> SelectById(long userId, long sysUserId) => await _userRepository.SelectById(userId, sysUserId);
    }
}
