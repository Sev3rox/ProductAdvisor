using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages
{
    public class ProfileModel : PageModel
    {

        [BindProperty]
        public Account account { get; set; }

        private ShopContext db;
        static string  uss;
        static string maill;
        public string Msg;
        public static Account acuu;
        public ProfileModel(ShopContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> OnGet()
        {
            var usernamee = HttpContext.Session.GetString("username");
            account = db.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            acuu = account;
            if(account==null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (account.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
           
            var username = HttpContext.Session.GetString("username");
            

            uss = account.Username;
            maill = account.Email;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(account.Password))
            {
         
                account.Password = db.Accounts.AsNoTracking().SingleOrDefault(a => a.Id == account.Id).Password;
            }
            else
            {
                account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            }

            var acc = login(account.Username);
            var mail = logine(account.Email);
            if ((acc == null||uss==account.Username) && (mail == null||maill==account.Email))
            {

                var ac = acuu;
                ac.Email = account.Email;
                ac.Password = account.Password;
                ac.Username = account.Username;
                db.SaveChanges();
                db.Entry(ac).State = EntityState.Modified;
                db.SaveChanges();
                HttpContext.Session.SetString("username", account.Username);
                return RedirectToPage("Welcome");
                
            }
            else
            {
                if (acc != null&& uss != account.Username)
                    Msg = Msg + "Ten username jest juz zarejestrowany \n";
                if (mail != null&& maill != account.Email)
                    Msg = Msg + "Ten email jest ju¿ zarejestrowany";
                return Page();
            }


        }


        private Account login(string username)
        {
            var accountt = db.Accounts.AsNoTracking().SingleOrDefault(a => a.Username.Equals(username));
            if (accountt != null)
            {
                return accountt;
            }
            return null;
        }

        private Account logine(string email)
        {
            var accountt = db.Accounts.AsNoTracking().SingleOrDefault(a => a.Email.Equals(email));
            if (accountt != null)
            {
                return accountt;
            }
            return null;
        }

    }
}
