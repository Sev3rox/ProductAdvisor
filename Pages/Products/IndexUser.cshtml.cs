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

namespace webapp.Pages.Products
{

    public class IndexUserModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public IndexUserModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        public IList<Product> Product { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }


        [BindProperty(SupportsGet = true)]
        public string CategoryTerm { get; set; }


        [BindProperty(SupportsGet = true)]
        public int SelectedTags { get; set; }

        public IList<Product> catss;
        public IList<Product> catsss;
        public List<Product> cats;
        public IList<Product> catssss;
        public List<Product_Category> pclist;
        public Account account { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
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
            catsss = catss;
            catsss = catsss.Where(j => DateTime.Compare(j.Premiera, DateTime.Now) < 0 && j.Premiera != null).ToList();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                catsss = catss.Where(p => p.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                                            p.Description.ToLower().Contains(SearchTerm.ToLower())
                                            ).ToList();
            }


            var categos = _context.Category.Where(item => item.Products.Any(j => j.Category.cName.ToLower().Contains(CategoryTerm.ToLower())));
            //var prods = _context.Product.Where()
            pclist = _context.Product_Category.ToList();
            catssss = new List<Product>();
            if (!string.IsNullOrEmpty(CategoryTerm))
            {
                foreach (Product x in catsss)
                {
                    foreach (Product_Category y in pclist)
                    {
                        foreach (Category z in _context.Category)
                        {
                            if (x.Id == y.ProductId && z.cName.ToLower() == CategoryTerm.ToLower() && z.ID == y.CategoryId)
                                catssss.Add(x);
                        }
                    }
                }
                catsss = catssss;
            }



            //_context.Entry(account.Blocked).CurrentValues.SetValues(Company);

            switch (SelectedTags)
            {
                case 1:
                    catsss = catsss.OrderBy(s => s.Id).ToList();
                    break;
                case 2:
                    catsss = catsss.OrderByDescending(s => s.Price).ToList();
                    break;
                case 3:
                    catsss = catsss.OrderBy(s => s.Price).ToList();
                    break;
                default:
                    catsss = catsss.OrderByDescending(s => s.Id).ToList();
                    break;
            }
            return Page();
        }
    }
}
