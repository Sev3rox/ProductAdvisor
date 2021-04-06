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
    public class DetailsModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public DetailsModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

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
    }
}
