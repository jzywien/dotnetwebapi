using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetWebApi.Data.Customers.LLC.DataModels
{
    public class Customer
    {
        public Customer() { }

        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public Address Address { get; set; }
    }
}