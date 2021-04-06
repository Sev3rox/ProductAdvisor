using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace webapp.Pages.Companies
{
    public class IndexModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public IndexModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Company> catss;
        public IList<Company> catsss;
        public Account account { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");





            catss = await _context.Company.ToListAsync();
            var username = HttpContext.Session.GetString("username");
            if (username == null)
                return Page();
            account = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            var companys = _context.Company.Where(item => item.BlockedBy.Any(j => j.AccountId == account.Id));
            List<Company>  cats = _context.Company.Where(item => item.BlockedBy.Any(j => j.AccountId == account.Id)).ToList();
            foreach (Company x in cats)
            {
                if (catss.Contains(x))
                { catss.Remove(x); }
            }
            catsss = catss;
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                catsss = catss.Where(p => p.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                                            p.Description.ToLower().Contains(SearchTerm.ToLower())
                                            ).ToList();
            }
            //_context.Entry(account.Blocked).CurrentValues.SetValues(Company);

            return Page();

        }
    }
}
