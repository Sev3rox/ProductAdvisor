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

namespace webapp.Pages.Prawa
{
    public class DetailsModel : PageModel
{
    private readonly webapp.Data.ShopContext _context;

    public DetailsModel(webapp.Data.ShopContext context)
    {
        _context = context;
    }

        public string all;
        public List<Decorations> cats;
        public static int idd;


        [BindProperty]
        public Account Account { get; set; }
        public string lastpage;
        public int pidd;

        public async Task<IActionResult> OnGetAsync(int? id, string userr, string xpom, int pid)
        {
            if (pid != 0)
            {
                pidd = pid;
            }
            var xx = 12;
            lastpage = null;
            if (xpom != null)
            {
                lastpage = xpom;
            }

            if (id == 0)
            {
                var accountttt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(userr));
                id = accountttt.Id;
            }
            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
        

        

        var listt = _context.Decorations;
            List<Decorations> catss = new List<Decorations>(listt);
            var categories = _context.Decorations.Where(item => item.Accounts.Any(j => j.AccountId == id));
            cats = new List<Decorations>(categories);
            foreach (Decorations x in cats)
            {
                if (catss.Contains(x))
                { catss.Remove(x); }
             //   all = all + " " + x.imie + " " + x.nazwisko + " " + x.nr_telefonu + "\n";
            }
            idd = (int)id;
            ViewData["WorkID2"] = new SelectList(cats, "Id", "Name");
            ViewData["WorkID"] = new SelectList(catss, "Id", "Name");

            if (id == null)
        {
            return NotFound();
        }

        Account = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);

        if (Account == null)
        {
            return NotFound();
        }
        return Page();
    }

        public async Task<IActionResult> OnPostAsync(string submitButton)
        {

            switch (submitButton)
            {
                case "Dodaj odznaczenie":
                    {
                        var procat = new Account_Decorations
                        {
                            AccountId = idd,
                            DecorationsId=Account.Id
                        };


                        if ((_context.Account_Decorations.Find(procat.AccountId, procat.DecorationsId)) != null)
                        {

                            return RedirectToPage("Details", new { id = idd });
                        }
                        else
                        {
                            _context.Account_Decorations.Add(procat);
                            await _context.SaveChangesAsync();
                            return RedirectToPage("Details", new { id = idd });
                        }

                    }
                case "Usuñ odznaczenie":
                    {

                        var procat = _context.Account_Decorations.First(row => row.AccountId == idd && row.DecorationsId == Account.Id);

                        _context.Account_Decorations.Remove(procat);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Details", new { id = idd });
                    }            
                default:
                    return RedirectToPage("Details", new { id = idd });




            }
        }


    }
}
