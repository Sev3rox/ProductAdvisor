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
    public class UlubModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public UlubModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Product> IProduct { get; set; }
        public Product Product { get; set; }
        [BindProperty]
        public Account Account { get; set; }
        public Account account { get; set; }
        public List<Product> cats;
        public List<Product> catss;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");

            IProduct = await _context.Product.ToListAsync();
            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            var listt = _context.Product;
            catss = new List<Product>(listt);
            var username = HttpContext.Session.GetString("username");
            account = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            var products = _context.Product.Where(item => item.UlubioneBy.Any(j => j.AccountId == id));
            cats = new List<Product>(products);
            foreach (Product x in cats)
            {
                if (catss.Contains(x))
                { catss.Remove(x); }
            }
            ViewData["CategoryID2"] = new SelectList(cats, "ID", "cName");
            ViewData["CategoryID"] = new SelectList(catss, "ID", "cName");
            _context.SaveChanges();
            //_context.Entry(account.Blocked).CurrentValues.SetValues(Company);
            var procat = new Ulubione
            {
                AccountId = account.Id,
                ProductId = Product.Id
            };

            _context.Ulubione.Add(procat);
            await _context.SaveChangesAsync();
            return RedirectToPage("./IndexUser");
        }
    }
}
