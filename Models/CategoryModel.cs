using KKTraders.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="This Field is required")]
        [StringLength(30)]
        [Display( Name ="Name the Category")]
        public string Title { get; set; }
        public List<Category> category { get; set; }

       

    }

}
