using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStockApp.Core.Domain.Common;

namespace MyStockApp.Core.Domain.Entities
{
    public class User : AuditableBaseEntity 
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        //A user can has many products
        public ICollection<Product> Products { get; set; }
    }
}
