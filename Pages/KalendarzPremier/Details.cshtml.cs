using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace webapp.Pages.KalendarzPremier
{
    public class DetailsModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public DetailsModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var listt = _context.Category;
            List<Category> catss = new List<Category>(listt);
            var categories = _context.Category.Where(item => item.Products.Any(j => j.ProductId == id));
            cats = new List<Category>(categories);
            foreach (Category x in cats)
            {
                if (catss.Contains(x))
                { catss.Remove(x); }
                all = all + " " + x.cName;
            }
            idd = (int)id;
            ViewData["CategoryID2"] = new SelectList(cats, "ID", "cName");
            ViewData["CategoryID"] = new SelectList(catss, "ID", "cName");
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.Include(p => p.Company).Include(z => z.ReviewsProduct).FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();

        }

        [BindProperty]
        public Product Product { get; set; }


        public static int idd;

        public string all;
        public List<Category> cats;
    }
}
