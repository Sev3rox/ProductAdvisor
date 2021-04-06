using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Glosowanie
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Miesiąc")]
        public string Month { get; set; }
        [Display(Name = "Rok")]
        public string Year { get; set; }

        public ICollection<ProductGlosowanie> Glosowanies { get; set; }
        public ICollection<AccountGlosowanie> Accounts { get; set; }
    }
}
