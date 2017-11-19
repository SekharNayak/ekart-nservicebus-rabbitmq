using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKart.Messages
{
    public class ProcessOrderCommand
    {
        public string Address { get; set; }

        public int CusitomerId { get; set; }

        public string EmailId { get; set; }

        public string Price { get; set; }
    }
}
