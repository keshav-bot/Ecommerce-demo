using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Data
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double salary { get; set; }
        public string JobDescription { get; set; }
        public ICollection<Employee> Employee  { get; set; }
    }
}
