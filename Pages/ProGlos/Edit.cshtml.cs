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

namespace webapp.Pages.ProGlos
{
    public class EditModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public EditModel(webapp.Data.ShopContext context)
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
           ViewData["GlosowanieId"] = new SelectList(_context.Głosowanie, "Id", "Id");
           ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Description");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProductGlosowanie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductGlosowanieExists(ProductGlosowanie.GlosowanieId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductGlosowanieExists(int id)
        {
            return _context.ProductGlosowanie.Any(e => e.GlosowanieId == id);
        }
    }
}
