﻿using System;
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
    public class CreateAdminModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;

        public CreateAdminModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Forum Forum { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {


            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (accountt == null)
                return RedirectToPage("../Common/NoAccessNotLoged");
            if (accountt.role == 0)
                return RedirectToPage("../Common/NoAccessUser");
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
            Comment.Account1.Username = HttpContext.Session.GetString("username");
            if (Comment.Account1.Username == null)
            {
                Comment.Account1.Username = "niezalogowany";
            }
            string adres = "/ForumAdministratora/Details?id=" + Forum.ID;
            _context.Commment.Add(Comment);
            await _context.SaveChangesAsync();

            return Redirect(adres);
        }
    }
}
