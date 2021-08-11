
using KKTraders.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models.ViewModel
{
    public class ProductCategoryViewModel
    {
        public IList<Product> product { get; set; }
        public IList<Category> category { get; set; }
    }
}
