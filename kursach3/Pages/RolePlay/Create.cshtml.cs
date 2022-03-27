#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kursach3.Data;
using kursach3.ViewModels;
using Microsoft.AspNetCore.Identity;
using kursach3.Models;

namespace kursach3.Pages.RolePlay
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
        public RolePlaysViewModel RolePlaysViewModel { get; set; }
        public Models.RolePlay RolePlay { get; set; }
        public Models.Character Character { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            RolePlay = new Models.RolePlay()
            {
                Description = RolePlaysViewModel.Description,
                Name = RolePlaysViewModel.Name,
                Master = user,
                MasterId = user.Id
            };
            if (RolePlay.Name == null || RolePlay.Description == null) return Page();

            _context.RolePlays.Add(RolePlay);
            await _context.SaveChangesAsync();

            int RolePlayId = _context.RolePlays.Max(i => i.RolePlayId);
            RolePlay.RolePlayId = RolePlayId;

            Character = new Character()
            {
                RolePlay = RolePlay,
                RolePlayId = RolePlay.RolePlayId,
                UserId = user.Id,
                User = user,
                СharacterName = RolePlaysViewModel.CharacterName
            };
            if (Character.СharacterName == null) return Page();

            _context.Characters.Add(Character);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
