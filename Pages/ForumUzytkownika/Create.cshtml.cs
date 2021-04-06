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

namespace webapp.Pages.ForumUzytkownika
{
    public class CreateModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public CreateModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");

            return Page();
        }

        [BindProperty]
        public Forum Forum { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Forum.data = DateTime.Now;
            Forum.userr= HttpContext.Session.GetString("username");
            if(Forum.userr==null)
            {
                Forum.userr = "niezalogowany";
            }
            _context.Forum.Add(Forum);
            await _context.SaveChangesAsync();
            var userr = _context.Accounts.First(a => a.Username == HttpContext.Session.GetString("username"));
            userr.posty++;

            if (userr.posty == 10)
            {
                var dec = _context.Decorations.First(a => a.Name == "10Forum");
                var prodeco = new Account_Decorations
                {
                    AccountId = userr.Id,
                    DecorationsId = dec.Id

                };
                _context.Account_Decorations.Add(prodeco);
                await _context.SaveChangesAsync();
            }
            if (userr.posty == 100)
            {
                var dec = _context.Decorations.First(a => a.Name == "100Forum");
                var prodeco = new Account_Decorations
                {
                    AccountId = userr.Id,
                    DecorationsId = dec.Id

                };
                _context.Account_Decorations.Add(prodeco);
                await _context.SaveChangesAsync();
            }
            if (userr.posty == 1000)
            {
                var dec = _context.Decorations.First(a => a.Name == "1000Forum");
                var prodeco = new Account_Decorations
                {
                    AccountId = userr.Id,
                    DecorationsId = dec.Id

                };
                _context.Account_Decorations.Add(prodeco);
                await _context.SaveChangesAsync();
            }

            _context.Attach(userr).State = EntityState.Modified;
            return RedirectToPage("./Index");
        }
    }
}
