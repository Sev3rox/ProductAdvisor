using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.ProductsDescription
{
    public class CreateModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public CreateModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id)
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
         
            if (id == null)
            {
                return NotFound();
            }
            ProductDescription = new ProductDescription();
            ProductDescription.ProductId = id??0;

            ViewData["ProductId"] = new SelectList(_context.Product.Where(p=>p.Id==id), "Id", "Description");

            Product = _context.Product.Where(p => p.Id == id).ToList();
            Product[0].Company =  _context.Company.FirstOrDefault(p => p.ID == Product[0].CompanyID);

            return Page();
        }

        [BindProperty]
        public ProductDescription ProductDescription { get; set; }
        public List<Product> Product{ get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProductDescription.Add(ProductDescription);
            await _context.SaveChangesAsync();

           // return RedirectToPage("./Index");
            return RedirectToPage("/Products/DetailsUser", new { id = ProductDescription.ProductId });
        }
    }
}