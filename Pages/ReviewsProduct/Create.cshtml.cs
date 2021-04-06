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

namespace webapp.Pages.ReviewsProduct
{
    public class CreateModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public CreateModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product product { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            ReviewProduct rew=null;
            if(_context.ReviewProduct.Any(a => a.ProductID == id))
            rew = _context.ReviewProduct.First(a => a.ProductID == id);
            int pomid = (int)id;
            if (rew != null)
            {
                if (_context.Account_Review.Find(Convert.ToSingle(accountt.Id), Convert.ToSingle(id))!=null)
                {
                   return Redirect("/Products/DetailsUser?id=" + pomid);
                }
                else
                {
                    
                }
            }
            else
            {
          
            }




            if (id == null)
            {
                return NotFound();
            }
            product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            //product = await _context.Product.Include(p => p.Categories).Include(p => p.CommentsProducts).FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return Page();
        }

        [BindProperty]
        public ReviewProduct ReviewProduct { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            _context.ReviewProduct.Attach(ReviewProduct);
            ReviewProduct.ProductID = product.Id;
            ReviewProduct.data = DateTime.Now;
            ReviewProduct.userr = HttpContext.Session.GetString("username");
            if (ReviewProduct.userr == null)
            {
                ReviewProduct.userr = "niezalogowany";
            }
            string adres = "/Products/DetailsUser?id=" + product.Id;
            _context.ReviewProduct.Add(ReviewProduct);
            await _context.SaveChangesAsync();
            product = await _context.Product
                .Include(p => p.Company).FirstOrDefaultAsync(m => m.Id == ReviewProduct.ProductID);
            product.opinion = ReviewProduct.Number+product.opinion;
            product.quantity = product.quantity + 1;

            _context.Attach(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var userr = _context.Accounts.First(a => a.Username == HttpContext.Session.GetString("username"));
            userr.oceny++;

            if (userr.oceny == 10)
            {
                var dec = _context.Decorations.First(a => a.Name == "10Oceny");
                var prodeco = new Account_Decorations
                {
                    AccountId = userr.Id,
                    DecorationsId = dec.Id

                };
                _context.Account_Decorations.Add(prodeco);
                await _context.SaveChangesAsync();
            }
            if (userr.oceny == 100)
            {
                var dec = _context.Decorations.First(a => a.Name == "100Oceny");
                var prodeco = new Account_Decorations
                {
                    AccountId = userr.Id,
                    DecorationsId = dec.Id

                };
                _context.Account_Decorations.Add(prodeco);
                await _context.SaveChangesAsync();
            }
            if (userr.oceny == 1000)
            {
                var dec = _context.Decorations.First(a => a.Name == "1000Oceny");
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

            userr.recenzje++;

            if (userr.recenzje == 10)
            {
                var dec = _context.Decorations.First(a => a.Name == "10Recenzje");
                var prodeco = new Account_Decorations
                {
                    AccountId = userr.Id,
                    DecorationsId = dec.Id

                };
                _context.Account_Decorations.Add(prodeco);
                await _context.SaveChangesAsync();
            }
            if (userr.recenzje == 100)
            {
                var dec = _context.Decorations.First(a => a.Name == "100Recenzje");
                var prodeco = new Account_Decorations
                {
                    AccountId = userr.Id,
                    DecorationsId = dec.Id

                };
                _context.Account_Decorations.Add(prodeco);
                await _context.SaveChangesAsync();
            }
            if (userr.recenzje == 1000)
            {
                var dec = _context.Decorations.First(a => a.Name == "1000Recenzje");
                var prodeco = new Account_Decorations
                {
                    AccountId = userr.Id,
                    DecorationsId = dec.Id

                };
                _context.Account_Decorations.Add(prodeco);
                await _context.SaveChangesAsync();
            }

            _context.Attach(userr).State = EntityState.Modified;

            ReviewProduct rew = null;
            if (_context.ReviewProduct.Any(a => a.ProductID == product.Id))
                rew = _context.ReviewProduct.First(a => a.ProductID == product.Id);

            var procat = new Account_Review
            {
                AccountId = Convert.ToSingle(userr.Id),
                number = Convert.ToSingle(ReviewProduct.Number),
                ProductId= Convert.ToSingle(rew.ProductID)
            };


            if ((_context.Account_Review.Find(Convert.ToSingle(procat.AccountId), Convert.ToSingle(procat.ProductId)) != null))
            {

               
            }
            else
            {
                _context.Account_Review.Add(procat);
                await _context.SaveChangesAsync();

            }


            await _context.SaveChangesAsync();
            return Redirect(adres);
        }
        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
