using System;
using System.Collections.Generic;

namespace Scheduling.Contract.Models
{
    public class Schedule
    {
        public long? ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public long CustomerId { get; set; }
        public string Employees { get; set; }
        public long? Id => ScheduleId;
        public Customer Customer { get; set; } = new Customer();
    }
    public class SchedulePaging
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<int> Employees = new List<int>();
        public List<int> OrganizationUnits = new List<int>();
        public int ScheduleGroupId { get; set; }
    }
}
