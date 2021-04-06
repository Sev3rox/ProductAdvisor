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

namespace webapp.Pages.ForumUzytkownika
{
    public class DetailsModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public DetailsModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Forum Forum { get; set; }
        public Comment comment;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (id == null)
            {
                return NotFound();
            }

            //Forum = await _context.Forum.FirstOrDefaultAsync(m => m.ID == id);
            Forum = await _context.Forum.Include(p => p.Comments).ThenInclude(x => x.Forum).FirstOrDefaultAsync(m => m.ID == id);
            //Category = await _context.Category.Include(p => p.Products).ThenInclude(x => x.Category).FirstOrDefaultAsync(m => m.ID == id);

            if (Forum == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
