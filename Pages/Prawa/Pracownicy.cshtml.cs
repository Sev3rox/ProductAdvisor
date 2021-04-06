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

namespace webapp.Pages.Prawa
{
    public class PracownicyModel : PageModel
    {
        private ShopContext _context;

        public PracownicyModel(ShopContext context)
        {
            _context = context;
        }
        public IList<Account> Account { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            Account = await _context.Accounts.Where(i => i.role == 1).ToListAsync();

            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
            if (accountt.role == 1)
                return RedirectToPage("../Common/NoAccessWorker");
            return Page();

        }
    }
}
