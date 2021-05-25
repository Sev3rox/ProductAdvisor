using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.ForumAdministratora
{

    public class ZmianaPrawModel : PageModel
    {
        private ShopContext _context;
        public ZmianaPrawModel(ShopContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Account Account { get; set; }
        public async Task<IActionResult> OnGetAsync(int? idForum, string idUser, string idRole)
        {
            string u = idUser;
            Account = await _context.Accounts.FirstOrDefaultAsync(m => m.Username == u);
            if (idRole == "odblokujKomentowanie" || idRole == "zablokujKomentowanie")
            {
                int pom = Account.role2;
                if (Account.role2 == 2)
                    Account.role2 = 0;
                else
                    Account.role2 = 2;
                await _context.SaveChangesAsync();
            }
            else if (idRole == "zbanuj" || idRole == "odbanuj")
            {

                if (Account.role == 0)
                {

                    Account.role2 = 0;
                    Account.role = 2;
                }
                else if (Account.role == 2)
                    Account.role = 0;
                await _context.SaveChangesAsync();
            }
            else if (idRole == "wyroznij" || idRole == "cofnijWyroznienie")
            {
                Account = await _context.Accounts.FirstOrDefaultAsync(m => m.Username == u);
                int pom = Account.role2;
                if (pom == 0 || pom == 2)
                    Account.role2 = 1;
                else
                    Account.role2 = 0;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/ForumAdministratora/Details", new { id = idForum });
        }

    }
}
