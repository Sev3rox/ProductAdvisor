using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Pages.ProductsDescription
{
    public class SaveDescriptionModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public SaveDescriptionModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public ProductDescription Description { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public String PrevDesc { get; set; }
        public async Task<IActionResult> OnGetAsync(int? ProductId, int? DescriptionId)
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
            if (ProductId == null)
            {
                return NotFound();
            }


            Product = await _context.Product
                .Include(p => p.Company).FirstOrDefaultAsync(m => m.Id == ProductId);

            if (Product == null)
            {
                return NotFound();
            }
            Description = await _context.ProductDescription.FirstOrDefaultAsync(m => m.Id == DescriptionId);
            PrevDesc = Product.Description;
            Product.Description = Description.Description;
            ViewData["CompanyID"] = new SelectList(_context.Company, "ID", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Attach(Product).State = EntityState.Modified;
            Product.Image = _context.Product.AsNoTracking().SingleOrDefault(a => a.Id == Product.Id).Image;
            //if (Upload != null)
            //{
            //    if (Upload.Length > 0)
            //    {
            //        Stream fileStream = Upload.OpenReadStream();
            //        byte[] buffer = new byte[fileStream.Length];
            //        fileStream.Read(buffer, 0, (int)fileStream.Length);
            //        fileStream.Close();
            //        Product.Image = buffer;
            //    }
            //}
            //else
            //{
            //    Product.Image = _context.Product.AsNoTracking().SingleOrDefault(a => a.Id == Product.Id).Image;
            //}

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
