using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class CommentReview
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Treść")]
        [Required]
        public string komentarz { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime data { get; set; }

        [Display(Name = "Dodał")]
        public string userr { get; set; }
        public bool hidden { get; set; }

        [Display(Name = "Komentarz")]
        public int ReviewProductID { get; set; }
        [Display(Name = "Komentarz")]
        public ReviewProduct ReviewProduct { get; set; }


    }
}
