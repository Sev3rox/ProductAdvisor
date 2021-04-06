using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.CommentsReview
{
    public class CreateModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public CreateModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReviewProduct ReviewProduct { get; set; }
        [BindProperty]
        public int x { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");

            
            if (id == null)
            {
                return NotFound();
            }

            ReviewProduct = await _context.ReviewProduct.FirstOrDefaultAsync(m => m.ID == id);
            var Product = _context.Product.Where(i => i.ReviewsProduct.Any(k => k.ID == ReviewProduct.ID));
            p = new List<Product>(Product);
            x = p[0].Id;
            if (ReviewProduct == null)
            {
                return NotFound();
            }
            return Page();
        }

        [BindProperty]
        public CommentReview CommentReview { get; set; }
        public List<Product> p { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            //var categories = _context.Category.Where(item => item.Products.Any(j => j.ProductId == id));
            var Product = _context.Product.Where(i => i.ReviewsProduct.Any(k => k.ID == ReviewProduct.ID));
            p = new List<Product>(Product);
            CommentReview.ReviewProductID = ReviewProduct.ID;
            CommentReview.data = DateTime.Now;
            CommentReview.userr = HttpContext.Session.GetString("username");
            if (CommentReview.userr == null)
            {
                CommentReview.userr = "niezalogowany";
            }
            string adres = "/Products/DetailsUser?id=" + p[0].Id ;
            _context.CommentReview.Add(CommentReview);
            await _context.SaveChangesAsync();
            var userr = _context.Accounts.First(a => a.Username == HttpContext.Session.GetString("username"));
            userr.komentarze++;

            if (userr.komentarze == 10)
            {
                var dec = _context.Decorations.First(a => a.Name == "10Komentarze");
                var prodeco = new Account_Decorations
                {
                    AccountId = userr.Id,
                    DecorationsId = dec.Id

                };
                _context.Account_Decorations.Add(prodeco);
                await _context.SaveChangesAsync();
            }
            if (userr.komentarze == 100)
            {
                var dec = _context.Decorations.First(a => a.Name == "100Komentarze");
                var prodeco = new Account_Decorations
                {
                    AccountId = userr.Id,
                    DecorationsId = dec.Id

                };
                _context.Account_Decorations.Add(prodeco);
                await _context.SaveChangesAsync();
            }
            if (userr.komentarze == 1000)
            {
                var dec = _context.Decorations.First(a => a.Name == "1000Komentarze");
                var prodeco = new Account_Decorations
                {
                    AccountId = userr.Id,
                    DecorationsId = dec.Id

                };
                _context.Account_Decorations.Add(prodeco);
                await _context.SaveChangesAsync();
            }

            _context.Attach(userr).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return Redirect(adres);
        }
    }
}
