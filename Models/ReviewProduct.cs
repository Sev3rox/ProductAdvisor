using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class ReviewProduct
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
        [Display(Name = "Ocena")]
        [Required]
        public int Number { get; set; }
        public bool hidden { get; set; }
        [Display(Name = "Produkt")]
        public int ProductID { get; set; }
        [Display(Name = "Produkt")]
        public Product Product { get; set; }
        public ICollection<CommentReview> CommentsReview { get; set; }
    }
}
