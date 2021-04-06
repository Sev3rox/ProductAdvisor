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

namespace webapp.Pages.Głosowaniee
{
    public class IndexNotLogedModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public IndexNotLogedModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Glosowanie> Głosowanie { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
        
            Głosowanie = await _context.Głosowanie.ToListAsync();
            return Page();
        }
    }
}
