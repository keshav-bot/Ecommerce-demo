using KKTraders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string EmployeeImageUrl { get; set; }
        
      
     

    }
}
