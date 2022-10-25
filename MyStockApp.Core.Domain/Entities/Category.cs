using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStockApp.Core.Domain.Common;

namespace MyStockApp.Core.Domain.Entities
{
    public class Category : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //A category can has many products
        public ICollection<Product>? Products { get; set; }
    }
}
