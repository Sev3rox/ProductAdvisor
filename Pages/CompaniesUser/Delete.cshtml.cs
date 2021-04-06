using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace webapp.Pages.CompaniesUser
{
    public class DeleteModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public DeleteModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Company Company { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");

            if (id == null)
            {
                return NotFound();
            }

            Company = await _context.Company.FirstOrDefaultAsync(m => m.ID == id);

            if (Company == null)
            {
                return NotFound();
            }
            return Page();
        }
        public Account account { get; set; }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var username = HttpContext.Session.GetString("username");
            account = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            var procat = _context.Blocked.First(row => row.AccountId == account.Id && row.CompanyId == id);

            _context.Blocked.Remove(procat);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
