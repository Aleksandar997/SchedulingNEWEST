using Entity.Base;
using System;
using sysUser = UserManagement.Models.User;

namespace Scheduling.Contract.Models
{
    public class User : sysUser
    {
        public Employee Employee { get; set; }
        public DateTime sysDTCreated { get; set; }
        public string RoleNames { get; set; }
        public User()
        {
            Employee = new Employee();
        }

        public User(Guid companyId)
        {
            CompanyId = companyId;
        }

    }
    public class UserPaging : BasePaging
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
