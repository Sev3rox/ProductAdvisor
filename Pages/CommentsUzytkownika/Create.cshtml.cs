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

namespace webapp.Pages.CommentsUzytkownika
{
    public class CreateModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public CreateModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Forum Forum { get; set; }

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

            Forum = await _context.Forum.FirstOrDefaultAsync(m => m.ID == id);

            if (Forum == null)
            {
                return NotFound();
            }
            return Page();
        }

        [BindProperty]
        public Comment Comment { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Comment.ForumID = Forum.ID;
            Comment.data = DateTime.Now;
            Comment.userr = HttpContext.Session.GetString("username");
            if (Comment.userr == null)
            {
                Comment.userr = "niezalogowany";
            }
            string adres = "/ForumUzytkownika/Details?id=" + Forum.ID;
            _context.Commment.Add(Comment);
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
