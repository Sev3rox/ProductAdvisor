using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.ForumNotLoged
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

        public async Task OnGetAsync()
        {
            Forum = await _context.Forum.Where(z=>z.titled==false).ToListAsync();
            ForumTitled = await _context.Forum.Where(z => z.titled == true).ToListAsync();
        }
    }
}
