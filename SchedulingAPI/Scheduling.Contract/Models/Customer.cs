using CodebookManagement.Attributes;
using CodebookManagement.Models;
using SQLContext.Attributes;
using SQLContext.Models;
using System;

namespace Scheduling.Contract.Models
{
    public class Customer
    {
        public int? CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
