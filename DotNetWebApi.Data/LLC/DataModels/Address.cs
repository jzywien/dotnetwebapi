﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetWebApi.Data.Customers.LLC.DataModels
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}