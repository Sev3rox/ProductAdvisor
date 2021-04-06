using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages
{

    public class SignUpModel : PageModel
    {

        [BindProperty]
        public Account account { get; set; }

        private ShopContext db;
        public string Msg;

        public SignUpModel(ShopContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            account = new Account();
        }
        public IActionResult OnPost()
        {
            var acc = login(account.Username);
            var mail = logine(account.Email);
            if (acc == null && mail == null)
            {
                account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
                account.role = 0;
                account.role2 = 0;
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToPage("LogIn");
            }
            else
            {
                if (acc != null)
                    Msg = Msg + "Ten username jest juz zarejestrowany \n";
                if (mail != null)
                    Msg = Msg + "Ten email jest ju¿ zarejestrowany";
                return Page();
            }

        }

        private Account login(string username)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (account != null)
            {
                return account;
            }
            return null;
        }

        private Account logine(string email)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Email.Equals(email));
            if (account != null)
            {
                return account;
            }
            return null;
        }

    }
}