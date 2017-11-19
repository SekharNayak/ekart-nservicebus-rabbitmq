using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eKart.Rest.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Address { get; set; }

        public int CusitomerId { get; set; }

        public string EmailId { get; set; }

        public string Price { get; set; }
    }
}