using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Decorations
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Zdjęcie")]
        //[Required(ErrorMessage = "Dodaj zdjęcie")]
        public byte[] Image { get; set; }
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Podaj nazwę")]
        public string Name { get; set; }

        public ICollection<Account_Decorations> Accounts { get; set; }

    }
}
/*
 
 10 100 1000 komentarzy
 10 100 1000 recenzji
 10 100 1000 ocen
 10 100 1000 postow na forum
-znawca produktów
-znawca firm
-znawca kategori
-pro recenzent
-aktywny user
-przyjazny user
-zangazowany forumowicz
-zangazowany komentator


Plain
text size 60 wymiary 80 80
three dimensional sharp
rectangle
 brąz #6E2E00
 silver #D1C9C9
 złoto #FFC400


1 10 100(zglasza opisy/poprawia)

 
 
 
 
 
 
 */