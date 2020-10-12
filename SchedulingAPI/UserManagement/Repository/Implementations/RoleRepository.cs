using Common.Extensions;
using Entity.Base;
using SQLContext;
using SQLContext.Factories;
using SQLContext.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Repository.Implementations
{
    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        public RoleRepository(string connectionString) : base(connectionString) { }

        public async Task<ResponseBase<Role>> SelectById(int roleId, long userId) =>
            await QueryMultipleAsync(
                "[dbo].[Role_SelectById]",
                new 
                {
                    roleId,
                    userId
                },
                (reader) =>
                {
                    var role = roleId > 0 ? reader.ReadFirst<Role>() : new Role();
                    role.Menus = reader.Read<Menu>().ToTreeView(x => x.MenuId, x => x.OrderBy(y => y.Sort).ToList());
                    role.Permissions = reader.Read<Permission>().ToTreeView(x => x.PermissionId);
                    return role;
                }
            );

        public async Task<ResponseBase<int>> Save(Role role, long userId) =>
            await ExecuteScalarAsync<int>(
                "[dbo].[Role_Save]",
                new
                {
                    role.RoleId,
                    role.Name,
                    role.Code,
                    role.Active,
                    Permissions = ParameterHelper.ToUserDefinedTableType(role.Permissions.Where(x => x.Active).Select(x => new { value = x.PermissionId }), "IntList"),
                    Menus = ParameterHelper.ToUserDefinedTableType(role.Menus.Where(x => x.Active).Select(x => new { value = x.MenuId }), "IntList"),
                    userId
                }
            );
    }

}
