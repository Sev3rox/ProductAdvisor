using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Ulubione
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
