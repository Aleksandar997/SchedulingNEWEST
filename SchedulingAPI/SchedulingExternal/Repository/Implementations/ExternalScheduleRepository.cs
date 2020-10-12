using SchedulingExternal.Repository.Interfaces;
using SQLContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingExternal.Repository.Implementations
{
    public class ExternalScheduleRepository : RepositoryBase, IExternalScheduleRepository
    {
        public ExternalScheduleRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
