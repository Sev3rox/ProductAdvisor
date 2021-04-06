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
    public class BlockModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public BlockModel(webapp.Data.ShopContext context)
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
            ICompany = await _context.Company.ToListAsync();
            Company = await _context.Company.FirstOrDefaultAsync(m => m.ID == id);
            var listt = _context.Company;
            catss = new List<Company>(listt);
            var username = HttpContext.Session.GetString("username");
            account = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            var companys = _context.Company.Where(item => item.BlockedBy.Any(j => j.AccountId == id));
            cats = new List<Company>(companys);
            foreach (Company x in cats)
            {
                if (catss.Contains(x))
                { catss.Remove(x); }
            }
            ViewData["CategoryID2"] = new SelectList(cats, "ID", "cName");
            ViewData["CategoryID"] = new SelectList(catss, "ID", "cName");
            _context.SaveChanges();
            //_context.Entry(account.Blocked).CurrentValues.SetValues(Company);
            var procat = new Blocked
            {
                AccountId = account.Id,
                CompanyId = Company.ID
            };

            _context.Blocked.Add(procat);
            await _context.SaveChangesAsync();
            return Page();
        }
    }
}