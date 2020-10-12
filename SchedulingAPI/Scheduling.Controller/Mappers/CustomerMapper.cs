using Scheduling.Contract.Models;
using Scheduling.Controller.Requests;
using Scheduling.Controller.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Controller.Mappers
{
    public static class CustomerMapper
    {
        public static Customer MapModel(CustomerRequest request) =>
            new Customer()
            {
                CustomerId = request.CustomerId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            };

        public static CustomerView MapView(Customer model) =>
            new CustomerView()
            {
                CustomerId = model.CustomerId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

        public static IEnumerable<CustomerView> MapView(IEnumerable<Customer> model) =>
            model.Select(c => MapView(c));
    }
}
