using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class ProductGlosowanie
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int GlosowanieId { get; set; }
        public Glosowanie Głosowanie{ get; set; }

        public int liczba { get; set; }
    }
}
