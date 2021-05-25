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
    public class AddProductToListModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;
        [BindProperty]
        public List<Product> Products { get; set; }
        [BindProperty]
        public Product pom { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public AddProductToListModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {

            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
            Products = new List<Product>();
            Products = _context.Product.Where(z => z.forAdvertising == false).ToList();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Products = Products.Where(p => p.Name.ToLower().Contains(SearchTerm.ToLower())).ToList();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string submitButton)
        {
            switch (submitButton)
            {
                case "Dodaj":
                    {
                        var pomek = _context.Product.First(p => p.Id == pom.Id);
                        pomek.forAdvertising = true;
                        _context.Attach(pomek).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        if (_context.Product.Where(z=>z.forAdvertising==true).ToList().Count >= 4)
                            return RedirectToPage("Index");
                        else return RedirectToPage("AddProductToList");
                    }

                default:
                    return RedirectToPage("Index");
            }
        }
    }
}
