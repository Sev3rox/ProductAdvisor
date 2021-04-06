using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class AccountGlosowanie
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int GlosowanieId { get; set; }
        public Glosowanie Głosowanie { get; set; }
    }
}
