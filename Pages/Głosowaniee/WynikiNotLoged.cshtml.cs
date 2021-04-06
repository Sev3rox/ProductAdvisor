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

namespace webapp.Pages.Głosowaniee
{
    public class WynikiNotLogedModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public WynikiNotLogedModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        public List<Product> cats;
        public IList<Product> catss;
        public IList<Product> Product;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
           
            Product = await _context.Product
                .Include(p => p.Company).ToListAsync();
            cats = await _context.Product.Where(p => p.Glosowanies.Any(j => j.GlosowanieId == id)).OrderByDescending(j => j.Glosowanies.Single(i => i.GlosowanieId == id).liczba).ToListAsync();
            return Page();
        }
    }
}
