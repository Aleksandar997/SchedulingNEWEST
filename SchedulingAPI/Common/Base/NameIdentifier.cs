using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Base
{
    public class NameIdentifier
    {
        public long UserId { get; set; }
        public Guid? CompanyId { get; set; }

        public NameIdentifier(long userId, Guid? companyId)
        {
            UserId = userId;
            CompanyId = companyId;
        }
    }
}
