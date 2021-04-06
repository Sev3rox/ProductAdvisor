using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.CommentsReview
{
    public class DeleteModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public DeleteModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CommentReview CommentReview { get; set; }
        [BindProperty]
        public int x { get; set; }
        public static int bb;

    public async Task<IActionResult> OnGetAsync(int? id)
        {
           


            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");

            if (id == null)
            {
                return NotFound();
            }

            CommentReview = await _context.CommentReview.FirstOrDefaultAsync(m => m.ID == id);
            var gg = CommentReview.ReviewProductID;
            var cg = _context.ReviewProduct.First(a => a.ID == gg);
            x = cg.ProductID;
            bb = x;
            if (CommentReview == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CommentReview = await _context.CommentReview.FindAsync(id);

            if (CommentReview != null)
            {
                _context.CommentReview.Remove(CommentReview);
                await _context.SaveChangesAsync();
            }
            x = bb;
            return RedirectToPage("../Products/Details", new { id = x});
        }
    }
}
