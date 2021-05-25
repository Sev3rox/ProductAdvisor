using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Treść")]
        [Required]
        public string komentarz { get; set; }
        [Display(Name = "Data dodania")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime data { get; set; }
        [Display(Name = "Dodał")]
        public int Account1ID { get; set; }
        public Account Account1 { get; set; }
        public bool hidden { get; set; }
        [Display(Name = "Forum")]
        public int ForumID { get; set; }
        [Display(Name = "Forum")]
        public Forum Forum { get; set; }
    }
}
