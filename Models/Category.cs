using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Display(Name = "Nazwa kategori")]
        public string cName { get; set; }
        [Display(Name = "Skrót")]
        public string cskrot { get; set; }
        [Display(Name = "Opis")]
        public string copis { get; set; }
        public ICollection<Product_Category> Products { get; set; }
    }
}