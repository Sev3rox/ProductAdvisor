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

namespace webapp.Pages.Categories
{
    public class IndexUserModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public IndexUserModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
        
            Category = await _context.Category.ToListAsync();
            return Page();
        }
    }
}