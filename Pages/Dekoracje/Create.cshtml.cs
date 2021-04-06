using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Dekoracje
{
    public class CreateModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;
 
        public IFormFile Upload { get; set; }
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
            return Page();
        }

        [BindProperty]
        public Decorations Decorations { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
          
            if (Upload.Length > 0)
            {
                Stream fileStream = Upload.OpenReadStream();
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
                fileStream.Close();
                Decorations.Image = buffer;
            }

            _context.Decorations.Add(Decorations);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
