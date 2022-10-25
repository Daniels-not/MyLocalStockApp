using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.ViewModels.Categories
{
    public class SaveCategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must put the name of the category")]
        public string Name { get; set; }
        public string Description { get; set; }
 
    }
}
