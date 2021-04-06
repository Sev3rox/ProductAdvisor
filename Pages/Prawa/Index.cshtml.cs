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

namespace webapp.Pages.Prawa
{
    public class IndexModel : PageModel
    {
        private ShopContext _context;

        public IndexModel(ShopContext context)
        {
            _context = context;
        }
             

        public async Task<IActionResult> OnGetAsync()
        {
            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 1)
                return RedirectToPage("IndexWorker");
            if (accountt.role == 3)
                return RedirectToPage("IndexAdm");
            return RedirectToPage("../Common/NoAccessUser");


        }


       
    }
}


/*
 * role:
 * 0 user
 * 1 worker
 * 2 banned
 * 3 admin
 * 
 * role2:
 * 0 nic
 * 1 wyrozniony
 * 2 zablokowane komentarze
 * */