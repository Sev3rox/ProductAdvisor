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
    public class ProfileUserModel : PageModel
    {

        [BindProperty]
        public Account account { get; set; }

        private ShopContext db;
        public List<Decorations> cats;
        public ProfileUserModel(ShopContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> OnGet()
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = db.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 1||accountt.role==3)
                return RedirectToPage("../Common/NoAccessUser");
            var username = HttpContext.Session.GetString("username");
            account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));








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
            db.SaveChanges();
            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();
            HttpContext.Session.SetString("username", account.Username);
            return RedirectToPage("WelcomeUser");
        }

    }
}
