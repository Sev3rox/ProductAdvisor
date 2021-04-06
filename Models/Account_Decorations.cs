using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Account_Decorations
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int DecorationsId { get; set; }
        public Decorations Decorations { get; set; }
    }
}
