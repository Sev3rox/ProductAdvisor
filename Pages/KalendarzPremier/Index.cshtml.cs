﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public IndexModel(webapp.Data.ShopContext context)
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
        public IList<Product> catssss;
        public List<Product_Category> pclist;
        public List<Product> cats;
        public Account account { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {

            Product = await _context.Product
                .Include(p => p.Company).ToListAsync();
            catss = await _context.Product.ToListAsync();
            var username = HttpContext.Session.GetString("username");
            account = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (account == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (account.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
            var companys = _context.Company.Where(item => item.BlockedBy.Any(j => j.AccountId == account.Id));
            cats = _context.Product.Where(item => item.UlubioneBy.Any(j => j.AccountId == account.Id)).ToList();
            foreach (Product x in cats)
            {
                if (catss.Contains(x))
                { catss.Remove(x); }

            }
            catsss = catss;
            //var temp1 = new List<Product>(catsss);
            catsss = catsss.Where(j => DateTime.Compare(j.Premiera, DateTime.Now) > 0 || j.Premiera == null).ToList();
            /*foreach (Product x in temp1)
            {
                if (x.Premiera != null)
                    if (DateTime.Compare(x.Premiera, DateTime.Now) < 0)
                        catsss.Remove(x);
            }*/

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

            catsss = catsss.OrderBy(s => s.Premiera).ToList();
            return Page();

        }
    }
}
