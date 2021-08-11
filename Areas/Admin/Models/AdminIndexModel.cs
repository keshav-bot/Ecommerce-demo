using KKTraders.Data;
using KKTraders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Areas.Admin.Models
{
    public class AdminIndexModel
    {
        public int TotalUser { get; set; }
        public int AdminUser { get; set; }
        public int Employee { get; set; }
        public int NoOfProduct { get; set; }
        public int NoOfPendingOrder { get; set; }
        public ICollection<Employee> employee { get; set; }
        public ICollection<Product> product { get; set; }

        public int NotificationCount { get; set; }
        public int numberOfSupplier { get; set; }
      
    }
}
