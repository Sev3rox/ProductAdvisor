using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;
namespace webapp.Pages.Prawa
{
    public class WyróżnijModel : PageModel
    {
        private ShopContext _context;

        public WyróżnijModel(ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Account = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);
            int pom = Account.role2;
            if (pom==0||pom==2)
                Account.role2 = 1;
            else 
                Account.role2 = 0;
            await _context.SaveChangesAsync();

            if (pom == 0||pom==2)
                return RedirectToPage("Index");
            else
                return RedirectToPage("Wyróżnieni");
        }
    }
}
