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

namespace webapp.Pages.Głosowaniee
{
    public class DeleteModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public DeleteModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Glosowanie Głosowanie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");

            if (id == null)
            {
                return NotFound();
            }

            Głosowanie = await _context.Głosowanie.FirstOrDefaultAsync(m => m.Id == id);

            if (Głosowanie == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Głosowanie = await _context.Głosowanie.FindAsync(id);

            if (Głosowanie != null)
            {
                _context.Głosowanie.Remove(Głosowanie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
