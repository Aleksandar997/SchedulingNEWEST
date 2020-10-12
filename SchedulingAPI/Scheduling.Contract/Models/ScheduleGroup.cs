using System.Collections.Generic;

namespace Scheduling.Contract.Models
{
    public class ScheduleGroup 
    {
        public int ScheduleGroupId { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
    }
}
