using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class OrderConfirmEmail
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public String Body { get; set; }

    }
}
