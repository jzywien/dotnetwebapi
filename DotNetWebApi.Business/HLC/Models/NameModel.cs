using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetWebApi.Business.Customers.HLC.Models
{
    public class NameModel
    {
        public string First { get; set; }
        public string Last { get; set; }

        public string Full
        {
            get
            {
                return First + " " + Last;
            }
        }
    }
}