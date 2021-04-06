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
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Dekoracje
{
    public class EditModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;
        [BindProperty]
        public IFormFile Upload { get; set; }

        public EditModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Decorations Decorations { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");

            if (id == null)
            {
                return NotFound();
            }

            Decorations = await _context.Decorations.FirstOrDefaultAsync(m => m.Id == id);

            if (Decorations == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
        

            _context.Attach(Decorations).State = EntityState.Modified;


          
            if (Upload != null)
            {
                if (Upload.Length > 0)
                {
                    Stream fileStream = Upload.OpenReadStream();
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, (int)fileStream.Length);
                    fileStream.Close();
                    Decorations.Image = buffer;
                }
            }
            else
            {

               Decorations.Image = _context.Decorations.AsNoTracking().SingleOrDefault(a => a.Id == Decorations.Id).Image;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DecorationsExists(Decorations.Id))
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

        private bool DecorationsExists(int id)
        {
            return _context.Decorations.Any(e => e.Id == id);
        }
    }
}
