using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Pages.Products
{

    public class CompareProductsNotLogedModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;
        public CompareProductsNotLogedModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
          

            var cookieValue = Request.Cookies["MyCookie"];
            List<String> elem = null;
            if (cookieValue != null)
            {
                elem = cookieValue.Split(",").ToList();

                Product = await _context.Product.Where(item => elem.Contains(item.Id.ToString()))
                       .Include(p => p.Company).ToListAsync();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var cookieValue = Request.Cookies["MyCookie"];
            List<String> elem = null;
            if (cookieValue != null)
                elem = cookieValue.Split(",").ToList();
            elem.RemoveAt(elem.IndexOf(id.ToString()));
            string str = String.Join(",", elem);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddHours(5)
            };
            Response.Cookies.Append("MyCookie", str, cookieOptions);

            await _context.SaveChangesAsync();

            return RedirectToPage("./CompareProductsNotLoged");
        }
        public IList<Product> Product { get; set; }
    }
}
