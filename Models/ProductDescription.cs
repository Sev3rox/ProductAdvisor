using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class ProductDescription
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nowy opis produktu")]
        public string Description { get; set; }
        [Display(Name = "Produkt")]
        public int ProductId { get; set; }
        [Display(Name = "Produkt")]
        public Product Product { get; set; }
        public bool hasBeenRead { get; set; }
    }
}
