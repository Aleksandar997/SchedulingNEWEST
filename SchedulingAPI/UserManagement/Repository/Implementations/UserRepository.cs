using CompanyManagement.Models;
using Entity.Base;
using Entity.Models;
using SQLContext;
using SQLContext.Factories;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Repository.Implementations
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString) { }

        public async Task<ResponseBase<User>> LoginUser(string username, string password, int cultureId) =>
            await ExecuteReaderAsync(
                "[dbo].[sysUser_Login]",
                new { username, password, cultureId },
                (reader) =>
                {
                    var user = reader.ReadFirst<User>();
                    user.Roles = reader.Read<Role>().ToList();
                    user.Menus = reader.Read<Menu>().ToList();
                    user.Permissions = reader.Read<Permission>().ToList();
                    return user;
                }
            );
        //{
        //    using (var reader = SqlContextFactory.InstanceManual(ConnectionString))
        //    {
        //        var read = await reader.ExecuteManual("[dbo].[sysUser_Login]", new { username, password, cultureId });
        //        var res = ReadData(() => {
        //            var user = read.Read.ReadFirst<User>();
        //            user.Roles = read.Read.Read<Role>().ToList();
        //            user.Menus = read.Read.Read<Menu>().ToList();
        //            user.Permissions = read.Read.Read<Permission>().ToList();
        //            return user;
        //        });
        //        if (res.Messages.Find(x => x.Code == "password_expired") != null)
        //        {
        //            return ResponseBase<User>.ReturnResponse(res.Data, ResponseStatus.PasswordExpired, res.Messages);
        //        }
        //        return res;
        //    }
        //}

        public async Task<ResponseBase<User>> SelectById(long userId = 0) =>
            await QueryMultipleAsync(
                "[dbo].[sysUser_SelectById]",
                new { userId },
                (reader) =>
                {
                    var user = reader.Read<User, Company, User>((user, company) =>
                    {
                        user.Company = company;
                        user.CompanyId = company.CompanyId;
                        return user;
                    }, splitOn: "CompanyId").FirstOrDefault();
                    user.Roles = reader.Read<Role>().ToList();
                    user.Menus = reader.Read<Menu>().ToList();
                    user.Permissions = reader.Read<Permission>().ToList();
                    user.ChartMetaData = reader.Read<ChartMetaData>().ToList();
                    user.Theme = reader.ReadFirst<Theme>();
                    return user;
                }
            );

        public async Task<ResponseBase<long>> ForgottenPassword(UserCredentials userCredentials) =>
            await ExecuteReaderAsync(
                "[dbo].[sysUser_ForgottenPassword]",
                new
                {
                    userCredentials.UserName,
                    userCredentials.Email,
                    userCredentials.NewPassword
                },
                (reader) => reader.ReadFirst<long>()
            );

        public async Task<ResponseBase<long>> ChangePassword(PasswordModel passwordModel) =>
            await ExecuteReaderAsync(
                "[dbo].[sysUser_ChangePassword]",
                new
                {
                    passwordModel.UserName,
                    passwordModel.Password,
                    passwordModel.NewPassword,
                    passwordModel.IsAdmin
                },
                (reader) => reader.ReadFirst<long>()
            );
    }
}
