using System;

namespace Scheduling.Controller.Views
{
    public class ProductTypeView
    {
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Code { get; set; }
        public Guid CompanyId { get; set; }
        public UserView User { get; set; }
    }
}