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

namespace webapp.Pages.ForumAdministratora
{
    public class DetailsModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public DetailsModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        public List<Comment> Recenzje { get; set; }
        [BindProperty]
        public Forum Forum { get; set; }
        public Comment comment { get; set; }
        [BindProperty]
        public int z { get; set; }
        [BindProperty]
        public Account uzytkownik { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
            uzytkownik = await _context.Accounts.FirstOrDefaultAsync(m => m.Username == usernamee);
            if (id == null)
            {
                return NotFound();
            }
            
            Forum = await _context.Forum.Include(p => p.Comments).ThenInclude(x => x.Forum).FirstOrDefaultAsync(m => m.ID == id);
            Recenzje =  _context.Commment.Where(z => z.Forum.ID== id).ToList();

            if (Forum == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string submitButton, int? id)
        {
            switch (submitButton)
            {
                case "Odkryj":
                    {
                        comment = await _context.Commment
                            .FirstOrDefaultAsync(m => m.ID == Forum.ID);
                        comment.hidden = false;
                        _context.Attach(comment).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Details", new { id = id });
                    }
                case "Ukryj":
                    {

                        comment = await _context.Commment
                            .FirstOrDefaultAsync(m => m.ID == Forum.ID);
                        comment.hidden = true;
                        _context.Attach(comment).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Details", new { id = id });
                    }

                default:
                    return RedirectToPage("Details", new { id = id });
            }
        }
    }
}
