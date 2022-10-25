using MyStockApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Domain.Entities
{
    public class Document : AuditableBaseEntity
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
