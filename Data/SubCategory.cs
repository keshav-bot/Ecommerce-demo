using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Data
{
    public class SubCategory
    {
        public int Id { get; set; }
        public String SubCategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
