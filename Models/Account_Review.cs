using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Account_Review
    {
        public float AccountId { get; set; }
        public Account Account { get; set; }

        public float number { get; set; }
        public float ProductId { get; set; }
        public Product Product { get; set; }
    }
}
