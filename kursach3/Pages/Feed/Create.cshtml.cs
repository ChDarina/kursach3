#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kursach3.Data;
using kursach3.Models;
using Microsoft.AspNetCore.Identity;

namespace kursach3.Pages
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly kursach3.Data.ApplicationDbContext _context;

        public CreateModel(kursach3.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public kursach3.Models.Feed Feed { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            Feed.User = user;
            Feed.User.Id = user.Id;
            Feed.Timestamp = DateTime.Now;
            if (Feed.Content == null || Feed.Title == null) return Page();
            _context.Feed.Add(Feed);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
