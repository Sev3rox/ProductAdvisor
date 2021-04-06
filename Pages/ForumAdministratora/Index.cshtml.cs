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

namespace webapp.Pages.ForumAdministratora
{
    public class IndexModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public IndexModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<Forum> ForumTitled { get; set; }
        [BindProperty]
        public IList<Forum> Forum { get; set; }
        [BindProperty]
        public int z { get; set; }
        public Forum forum { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");


            Forum = await _context.Forum.Where(z => z.titled == false).ToListAsync();
            ForumTitled = await _context.Forum.Where(z => z.titled == true).ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string submitButton)
        {
            switch (submitButton)
            {
                case "odWyroznij":
                    {
                        forum = await _context.Forum
                            .Include(m => m.Comments).FirstOrDefaultAsync(m => m.ID == z);
                        forum.titled = false;
                        _context.Attach(forum).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Index");
                    }
                case "Wyroznij":
                    {
                        
                        forum =await  _context.Forum
                            .Include(m => m.Comments).FirstOrDefaultAsync(m => m.ID == z);
                        forum.titled = true;
                        _context.Attach(forum).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Index");
                    }
                
                default:
                    return RedirectToPage("Index");
            }
        }
    }

}
