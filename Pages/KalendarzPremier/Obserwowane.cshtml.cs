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

namespace webapp.Pages.KalendarzPremier
{
    public class ObserwowaneModel : PageModel
    {
        
            private readonly webapp.Data.ShopContext _context;

            public ObserwowaneModel(webapp.Data.ShopContext context)
            {
                _context = context;
            }
            public IList<Product> Product { get; set; }
            [BindProperty(SupportsGet = true)]
            public string SearchTerm { get; set; }
            [BindProperty(SupportsGet = true)]
            public int SelectedTags { get; set; }

            public IList<Product> catss;
            public IList<Product> catsss;
            public List<Product> cats;
            public Account account { get; set; }
            public async Task OnGetAsync()
            {

                Product = await _context.Product
                    .Include(p => p.Company).ToListAsync();
                catss = await _context.Product.ToListAsync();
                var username = HttpContext.Session.GetString("username");
                account = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
                var companys = _context.Company.Where(item => item.BlockedBy.Any(j => j.AccountId == account.Id));
                cats = _context.Product.Where(item => item.UlubioneBy.Any(j => j.AccountId == account.Id)).ToList();
                foreach (Product x in cats)
                {
                    if (catss.Contains(x))
                    { catss.Remove(x); }
                }
                catsss = cats;
                catsss = catsss.Where(j => DateTime.Compare(j.Premiera, DateTime.Now) >0 && j.Premiera != null).ToList();
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    catsss = cats.Where(p => p.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                                                p.Description.ToLower().Contains(SearchTerm.ToLower())
                                                ).ToList();
                }

                switch (SelectedTags)
                {
                    case 1:
                        Product = Product.OrderBy(s => s.Id).ToList();
                        break;
                    case 2:
                        Product = Product.OrderByDescending(s => s.Price).ToList();
                        break;
                    case 3:
                        Product = Product.OrderBy(s => s.Price).ToList();
                        break;
                    default:
                        Product = Product.OrderByDescending(s => s.Id).ToList();
                        break;
                }

            }
        }
}
