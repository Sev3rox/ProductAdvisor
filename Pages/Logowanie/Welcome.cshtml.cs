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
    public class WelcomeModel : PageModel
    {
        public Account account { get; set; }

        private ShopContext db;
        public WelcomeModel(ShopContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> OnGet()
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = db.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
            var username = HttpContext.Session.GetString("username");
            account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            return Page();
        }
    }
}
