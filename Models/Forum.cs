using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Forum
    {
        public int ID { get; set; }
        [Display(Name = "Tytuł")]
        public string name { get; set; }
        [Display(Name = "Treść")]
        public string tresc { get; set; }
        [Display(Name = "Data dodania")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime data { get; set; }
        [Display(Name = "Dodał")]
        public string userr { get; set; }
        public bool titled { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}

