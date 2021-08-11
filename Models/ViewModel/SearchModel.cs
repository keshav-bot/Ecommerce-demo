using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models.ViewModel
{
    public class SearchModel
    {
        public ICollection<SimillarSearchModel> simillarSearch { get; set; }
        public SearchViewModel Searchvm { get; set; }
    }
}
