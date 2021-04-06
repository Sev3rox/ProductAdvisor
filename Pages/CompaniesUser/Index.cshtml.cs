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

namespace webapp.Pages.CompaniesUser
{
    public class IndexModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public IndexModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Company> ICompany { get; set; }
        public Company Company { get; set; }
        [BindProperty]
        public Account Account { get; set; }
        public Account account { get; set; }
        public List<Company> cats;
        public List<Company> catss;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");


            ICompany = await _context.Company.ToListAsync();
            var listt = _context.Company;
            catss = new List<Company>(listt);
            var username = HttpContext.Session.GetString("username");
            account = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            var companys = _context.Company.Where(item => item.BlockedBy.Any(j => j.AccountId == account.Id));
            cats = new List<Company>(companys);
            foreach (Company x in cats)
            {
                if (catss.Contains(x))
                { catss.Remove(x); }
            }
            //_context.Entry(account.Blocked).CurrentValues.SetValues(Company);

            return Page();

        }
    }
}
