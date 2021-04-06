using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Blocked
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
