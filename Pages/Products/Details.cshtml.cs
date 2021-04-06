using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace webapp.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public DetailsModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ReviewProduct ReviewProduct { get; set; }
        public CommentReview CommentReview { get; set; }
        public double srednia { get; set; }
        public List<ReviewProduct> Recenzje { get; set; }
        public List<CommentReview> comrecenzje1 { get; set; }
        public string pomstr;

        public async Task<IActionResult> OnGetAsync(int? id, string f)
        {
            if (f == "xyz")
            {
                pomstr = "xyz";
            }
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser"); 
            var listt = _context.Category;
            List<Category> catss = new List<Category>(listt);
            var categories = _context.Category.Where(item => item.Products.Any(j => j.ProductId == id));
            cats = new List<Category>(categories);
            foreach (Category x in cats)
            {
                if (catss.Contains(x))
                { catss.Remove(x); }
                all = all + " " + x.cName;
            }
            idd = (int)id;
            ViewData["CategoryID2"] = new SelectList(cats, "ID", "cName");
            ViewData["CategoryID"] = new SelectList(catss, "ID", "cName");
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.Include(p => p.Company).Include(z => z.ReviewsProduct).FirstOrDefaultAsync(m => m.Id == id);

            var reviewProduct = _context.ReviewProduct.Where(i => i.ProductID == id).Include(p => p.CommentsReview);
            Recenzje = new List<ReviewProduct>(reviewProduct);

            iddd = Product.Id;
            if (Product.quantity != 0)
            { srednia = Product.opinion / Product.quantity; }
            else srednia = 0;

            if (Product == null)
            {
                return NotFound();
            }
            return Page();

        }

        [BindProperty]
        public Product Product { get; set; }


        public static int idd;
        public static int iddd;

        public string all;
        public List<Category> cats;
        [BindProperty]
        public int z { get; set; }
        public CommentReview commentsz { get; set; }
        public ReviewProduct reviewp { get; set; }


        public async Task<IActionResult> OnPostAsync(string submitButton, int? id)
        {
            switch (submitButton)
            {
                case "Dodaj Kategorie":
                    {
                        var procat = new Product_Category
                        {
                            ProductId = iddd,
                            CategoryId = Product.CompanyID
                        };


                        if ((_context.Product_Category.Find(procat.ProductId, procat.CategoryId)) != null)
                        {

                            return RedirectToPage("Details", new { id = idd });
                        }
                        else
                        {
                            _context.Product_Category.Add(procat);
                            await _context.SaveChangesAsync();
                            return RedirectToPage("Details", new { id = idd });
                        }

                    }
                case "Usuń Kategorie":
                    {

                        var procat = _context.Product_Category.First(row => row.ProductId == iddd && row.CategoryId == Product.Id);

                        _context.Product_Category.Remove(procat);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Details", new { id = idd });
                    }

                case "Ukryj Komentarz":
                    {

                        commentsz = await _context.CommentReview
                            .FirstOrDefaultAsync(m => m.ID == z);
                        commentsz.hidden = true;
                        _context.Attach(commentsz).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Details", new { id = idd });
                    }
                case "Odslon Komentarz":
                    {
                        commentsz = await _context.CommentReview
                                                    .FirstOrDefaultAsync(m => m.ID == z);
                        commentsz.hidden = false;
                        _context.Attach(commentsz).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Details", new { id = idd });
                    }
                case "Ukryj Recenzje":
                    {
                        reviewp = await _context.ReviewProduct
                            .FirstOrDefaultAsync(m => m.ID == z);
                        reviewp.hidden = true;
                        _context.Attach(reviewp).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Details", new { id = idd });
                    }
                case "Odslon Recenzje":
                    {
                        reviewp = await _context.ReviewProduct
                                                    .FirstOrDefaultAsync(m => m.ID == z);
                        reviewp.hidden = false;
                        _context.Attach(reviewp).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Details", new { id = idd });
                    }



                case "Dodaj do porównania":
                    {
                        var cookieValue = Request.Cookies["MyCookie"];
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddHours(5)
                        };
                        Response.Cookies.Append("MyCookie", cookieValue + "," + Product.Id.ToString(), cookieOptions);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Details", new { id = idd });
                    }
                default:
                    return RedirectToPage("Details", new { id = idd });
            }
        }

    }
}

