using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Logowanie
{
    public class LogInModel : PageModel
    {
        [BindProperty]
        public Account account { get; set; }
        public string Msg;

        private ShopContext db;
        public LogInModel(ShopContext _db)
        {
            db = _db;
        }

        public void OnGet()
        {
            account = new Account();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("Index");
        }

        public IActionResult OnPost()
        {
            var acc = login(account.Username, account.Password);
            if (acc == null)
            {
                Msg = "Invalid";
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("username", acc.Username);
                if(acc.role==0)
                return RedirectToPage("/Common/MainUser");

                if (acc.role == 2)
                {
                    HttpContext.Session.Clear();
                    Msg = "Banned";
                    return Page();
                }
                 

                    return RedirectToPage("/Common/MainWorker");
            }

        }

        private Account login(string username, string password)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
            }
            return null;

        }

    }
}
