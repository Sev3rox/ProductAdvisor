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

namespace webapp.Pages.ProGlos
{
    public class CreateModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public CreateModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
            var temp1= new SelectList(_context.Głosowanie, "Id", "Id");
            ViewData["GlosowanieId"] = temp1;
            var temp2 = new SelectList(_context.Product, "Id", "Description");
            ViewData["ProductId"] = temp2;
            return Page();
        }

        [BindProperty]
        public ProductGlosowanie ProductGlosowanie { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductGlosowanie pg = _context.ProductGlosowanie.SingleOrDefault(b => (b.GlosowanieId == ProductGlosowanie.GlosowanieId && b.ProductId == ProductGlosowanie.ProductId));
            if(pg==null)
            _context.ProductGlosowanie.Add(ProductGlosowanie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
