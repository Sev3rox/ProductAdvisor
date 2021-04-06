using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Account account { get; set; }
        public string Msg;
        public IList<Product> Product { get; set; }
        private ShopContext db;
        public IndexModel(ShopContext _db)
        {
            db = _db;
        }

        public void OnGet()
        {
            account = new Account();
            var produkty = db.Product.ToList();
            List<KeyValuePair<float, Product>> list = new List<KeyValuePair<float, Product>>();
            List<KeyValuePair<float, Product>> list3 = new List<KeyValuePair<float, Product>>();
            foreach (Product p in produkty)
            {
                float pom = 0;
                int i = 0;
                var lista = db.ReviewProduct.Where(a => a.ProductID == p.Id).ToList();
                foreach (ReviewProduct r in lista)
                {
                    pom += r.Number;
                    i++;
                }
                pom /= i;
                list.Add(new KeyValuePair<float, Product>(pom, p));
            }
            foreach(KeyValuePair<float, Product> tem in list)
            {
                if (tem.Key.ToString() != "NaN") list3.Add(tem);
            }
            list3 = list3.OrderByDescending(o => o.Key).ToList();
            int pom1 = list3.Count();
            Product = new List<Product>();
            for (int i = 0; i < Math.Min(10, pom1); i++)
            {
                Product.Add(list3.ElementAt(i).Value);
            }
            
            if (Product.Count < 5)
            {
                var produktyy = db.Product.ToList();
                Product.Clear();
                for(int i=0;i< Math.Min(10, produktyy.Count); i++)
                {
                    Product.Add(produktyy[i]);
                }
                
            }
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("Index");

        }

        public IActionResult OnPost()
        {
            var acc = login(account.Username, account.Password);
            if (acc == null)
            {
                Msg = "Invalid";
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("username", acc.Username);
                return RedirectToPage("/Logowanie/Welcome");
            }

        }

        private Account login(string username, string password)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
            }
            return null;

        }

    }
}