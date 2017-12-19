using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetWebApi.Customers.Delegates.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel() { }

        public Guid Guid { get; set; }

        /// <summary>
        /// This is the full name of the customer. First Name + Last Name
        /// </summary>
		[Required]
        public string Name { get; set; }
        public string Number { get; set; }
        public AddressViewModel Address { get; set; }
    }
}