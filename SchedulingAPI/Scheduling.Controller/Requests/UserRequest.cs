using Entity.Base;
using System;
using UserManagement.Models;

namespace Scheduling.Controller.Requests
{
    public class UserRequest : User
    {
        public EmployeeRequest Employee { get; set; }
        public DateTime sysDTCreated { get; set; }
        public string RoleNames { get; set; }
        public UserRequest()
        {
            Employee = new EmployeeRequest();
        }

        public UserRequest(Guid companyId)
        {
            CompanyId = companyId;
        }

    }
}
