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
    public class IndexModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public IndexModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Forum> ForumTitled { get;set; }
        public IList<Forum> Forum { get; set; }
        [BindProperty]
        public List<Product> Products { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            Products = _context.Product.Where(z => z.forAdvertising == true).ToList();
            Forum = await _context.Forum.Where(z=>z.titled==false).ToListAsync();
            ForumTitled = await _context.Forum.Where(z => z.titled == true).ToListAsync();
            return Page();
        }
    }
}
