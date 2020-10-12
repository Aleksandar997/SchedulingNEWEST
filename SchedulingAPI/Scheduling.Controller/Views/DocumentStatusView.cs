using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.Controller.Views
{
    public class DocumentStatusView
    {
        [PrimaryKey]
        public int DocumentStatusId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
