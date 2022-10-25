using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        /*
         AuditableBaseEntity is a class whish contains dates that can be call to auditate any 
            product/ category/ user insertion into the data base
         */
        public virtual int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
