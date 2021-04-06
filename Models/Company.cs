using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Company
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Logo")]
        public byte[] Image { get; set; }
        [Display(Name = "Nazwa")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Data założenia")]
        [Required]
        public DateTime Date { get; set; }
        [Display(Name = "Lokalizacja")]
        [Required]
        public string Location { get; set; }

        public ICollection<Blocked> BlockedBy { get; set; }
    }
}
