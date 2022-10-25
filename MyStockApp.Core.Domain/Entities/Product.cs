using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStockApp.Core.Domain.Common;

namespace MyStockApp.Core.Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double? Price { get; set; }

        public int CategoryId { get; set; }

        //A product can has many categories
        public Category? Category { get; set; }

        public int UserId { get; set; }

        //A product can be made by the user
        public User? User { get; set; }
        public ICollection<Document>? Documents { get; set; }
    }
}
