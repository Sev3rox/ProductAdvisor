using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace webapp.Pages.StronaGłosowania
{
    public class GlosModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;
        public Account account;
        public GlosModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var usernamee = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(usernamee));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");

            var username = HttpContext.Session.GetString("username");
            account = await _context.Accounts.SingleOrDefaultAsync(a => a.Username.Equals(username));
            DateTime date = DateTime.Now;
            int rok = date.Year;
            int miesiac = date.Month;
            Glosowanie gloso = await _context.Głosowanie.SingleAsync(j => j.Year == rok.ToString() && j.Month == miesiac.ToString());
            if (await _context.AccountGlosowanies.SingleOrDefaultAsync(a => a.AccountId == account.Id&&a.GlosowanieId==gloso.Id) != null)
            { return RedirectToPage("./Index"); }
            ProductGlosowanie ilosc = await _context.ProductGlosowanie.SingleAsync(a => a.ProductId == id && a.GlosowanieId == gloso.Id);
            ilosc.liczba++;
            AccountGlosowanie temp = new AccountGlosowanie();
            temp.AccountId = account.Id;
            temp.GlosowanieId = ilosc.GlosowanieId;
            _context.AccountGlosowanies.Add(temp);
            await _context.SaveChangesAsync();
            _context.Attach(ilosc).State = EntityState.Modified;
            return Page();
        }
    }
}
