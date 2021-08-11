using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models.ViewModel
{
    public class SearchViewModel
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public double  Price { get; set; }
        public string  Title { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
        public double Discount { get; set; }

        
    }
}
