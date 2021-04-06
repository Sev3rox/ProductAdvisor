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
    public class IndexNotLogedModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public IndexNotLogedModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
           
            Category = await _context.Category.ToListAsync();
            return Page();
        }
    }
}