using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetWebApi.Customers.Delegates.ViewModels
{
    public class AddressViewModel
    {
        public AddressViewModel() { }
        public string FullStreetAddress { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}