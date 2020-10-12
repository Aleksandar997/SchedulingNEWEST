using System;
using sysUser = UserManagement.Models.User;

namespace Scheduling.Controller.Views
{
    public class UserView : sysUser
    {
        public EmployeeView Employee { get; set; }
        public DateTime sysDTCreated { get; set; }
        public string RoleNames { get; set; }
        public UserView()
        {
            Employee = new EmployeeView();
        }

        public UserView(Guid companyId)
        {
            CompanyId = companyId;
        }

    }
}
