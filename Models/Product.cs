using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Zdjęcie")]
        [Required(ErrorMessage = "Dodaj zdjęcie")]
        public byte[] Image { get; set; }
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Podaj nazwę")]
        public string Name { get; set; }
        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Podaj cenę")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Podaj opis")]
        public string Description { get; set; }

        [Display(Name = "opinie")]
        public double opinion { get; set; }
        [Display(Name = "Liczba ocen")]
        public int quantity { get; set; }
        public ICollection<Product_Category> Categories { get; set; }
        public ICollection<ReviewProduct> ReviewsProduct { get; set; }

        [Display(Name = "Firma")]
        public Company Company { get; set; }
        [Display(Name = "Firma")]
        public int CompanyID { get; set; }

        public ICollection<Ulubione> UlubioneBy { get; set; }
        public ICollection<ProductGlosowanie> Glosowanies { get; set; }
        public ICollection<ProductDescription> ProductDescriptions { get; set; }
        public DateTime Premiera { get; set; }

    }
}
