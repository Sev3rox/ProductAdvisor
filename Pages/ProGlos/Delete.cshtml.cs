using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.ProGlos
{
    public class DeleteModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public DeleteModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductGlosowanie ProductGlosowanie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductGlosowanie = await _context.ProductGlosowanie
                .Include(p => p.Głosowanie)
                .Include(p => p.Product).FirstOrDefaultAsync(m => m.GlosowanieId == id);

            if (ProductGlosowanie == null)
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

            ProductGlosowanie = await _context.ProductGlosowanie.FindAsync(id);

            if (ProductGlosowanie != null)
            {
                _context.ProductGlosowanie.Remove(ProductGlosowanie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
