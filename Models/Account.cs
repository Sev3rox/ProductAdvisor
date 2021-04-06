using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
namespace webapp.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Username{ get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int role { get; set; }
        public int role2 { get; set; }
        public int posty { get; set; }
        public int komentarze { get; set; }
        public int oceny { get; set; }
        public int recenzje { get; set; }
        public ICollection<Account_Decorations> Decorations { get; set; }
        public ICollection<Blocked> Blockeds { get; set; }

        public ICollection<Ulubione> Ulubiones { get; set; }
        public ICollection<AccountGlosowanie> AccountGlosowanies { get; set; }
    }
}
