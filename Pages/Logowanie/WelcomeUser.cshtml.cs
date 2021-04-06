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
    public class WelcomeUserModel : PageModel
    {
        public Account account { get; set; }
        public List<Decorations> cats;
        private ShopContext db;
        public WelcomeUserModel(ShopContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> OnGet()
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = db.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            var username = HttpContext.Session.GetString("username");
            account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));


            var listt = db.Decorations;
            List<Decorations> catss = new List<Decorations>(listt);
            var categories = db.Decorations.Where(item => item.Accounts.Any(j => j.AccountId == accountt.Id));
            cats = new List<Decorations>(categories);
            foreach (Decorations x in cats)
            {
                if (catss.Contains(x))
                { catss.Remove(x); }
                //   all = all + " " + x.imie + " " + x.nazwisko + " " + x.nr_telefonu + "\n";
            }

            return Page();
        }
    }
}
