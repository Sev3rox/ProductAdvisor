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


namespace webapp.Pages.StronaGłosowania
{

    public class IndexModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public IndexModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        public IList<Product> Product { get; set; }
       // [BindProperty(SupportsGet = true)]
       // public string SearchTerm { get; set; }
       // [BindProperty(SupportsGet = true)]
       // public int SelectedTags { get; set; }

        public IList<Product> catss;
        public IList<Product> catsss;
        public List<Product> cats;
        public Account account { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");


            Product = await _context.Product
                .Include(p => p.Company).ToListAsync();
            catss = await _context.Product.ToListAsync();
            //var username = HttpContext.Session.GetString("username");
            //account = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            //var companys = _context.Company.Where(item => item.BlockedBy.Any(j => j.AccountId == account.Id));
            DateTime date = DateTime.Now;
            int rok = date.Year;
            int miesiac = date.Month;
            var a = rok.ToString();
            var b = miesiac.ToString();
            cats = _context.Product.Where(item => item.Glosowanies.Any(j => j.Głosowanie.Year == rok.ToString() && j.Głosowanie.Month == miesiac.ToString())).ToList();
            int i = 0;
            /*
            foreach (Product x in cats)
            {
                if (catss.Contains(x))
                { catss.Remove(x); }
            }*/
            //_context.Entry(account.Blocked).CurrentValues.SetValues(Company);
            return Page();
        }
    }
}
