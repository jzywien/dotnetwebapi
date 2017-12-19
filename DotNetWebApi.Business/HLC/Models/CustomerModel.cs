using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetWebApi.Business.Customers.HLC.Models
{
    public class CustomerModel
    {
        public CustomerModel() { }

        public Guid Guid { get; set; }

        public NameModel Name { get; set; }
        public string Number { get; set; }
        public AddressModel Address { get; set; }
    }
}