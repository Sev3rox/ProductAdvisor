using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.ProductsDescription
{
    public class DetailsModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public DetailsModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductDescription ProductDescription { get; set; }
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
            if (id == null)
            {
                return NotFound();
            }

            ProductDescription = await _context.ProductDescription
                .Include(p => p.Product).FirstOrDefaultAsync(m => m.Id == id);
            //  ProductDescription.Product.Company = await _context.Company.FirstOrDefaultAsync(p => p.ID == ProductDescription.Product.CompanyID);

            Product = _context.Product.Find(ProductDescription.ProductId);

            if (ProductDescription == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //var Prod = _context.Product.AsNoTracking().SingleOrDefault(a => a.Id == ProductDescription.ProductId);
            //var Product = _context.Product.Find(ProductDescription.ProductId);
            _context.Attach(_context.Product.Find(ProductDescription.ProductId)).State = EntityState.Modified;
            //Product.Image = _context.Product.AsNoTracking().SingleOrDefault(a => a.Id == Product.Id).Image;
            Product.Description = "ssss";
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
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
        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}

