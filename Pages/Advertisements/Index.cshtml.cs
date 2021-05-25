using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Pages.Advertisements
{
    public class IndexModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;
        public IndexModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        [BindProperty]
        public List<Product> Products { get; set; }
        [BindProperty]
        public Product pom { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
            Products = new List<Product>();
            Products = _context.Product.Where(z => z.forAdvertising == true).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string submitButton)
        {
            switch (submitButton)
            {
                case "Usuñ z listy":
                    {
                        Product pomek = _context.Product.First(p => p.Id == pom.Id);
                        pomek.forAdvertising = false;
                        _context.Attach(pomek).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        return RedirectToPage("Index");
                    }

                default:
                    return RedirectToPage("Index");
            }
        }
    }
}
