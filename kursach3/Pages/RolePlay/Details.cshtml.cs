#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kursach3.Data;
using kursach3.Models;
using kursach3.ViewModels;

namespace kursach3.Pages.RolePlay
{
    public class DetailsModel : PageModel
    {
        private readonly kursach3.Data.ApplicationDbContext _context;

        public DetailsModel(kursach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public static string RolePlayName { get; set; }
        public static string RolePlayMasterName { get; set; }
        public RolePlaysViewModel RolePlaysViewModel { get; set; }
        public Models.RolePlay RolePlay { get; set; }
        public Models.ApplicationUser User { get; set; }
        public IList<kursach3.ViewModels.CharacterViewModel> CharactersViewModel { get; set; }
        public IList<kursach3.Models.Character> Characters { get; set; }

        public async Task GetLists(int id)
        {
            RolePlay = await _context.RolePlays.FirstOrDefaultAsync(m => m.RolePlayId == id);
            RolePlayName = RolePlay.Name;
            User = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == RolePlay.MasterId);
            RolePlayMasterName = User.UserName;
            RolePlaysViewModel = new RolePlaysViewModel
            {
                Id = id,
                Name = RolePlay.Name,
                MasterUsername = User.UserName,
                Description = RolePlay.Description,
            };

            var Characterstemp = await _context.Characters.ToListAsync();
            CharactersViewModel = new List<CharacterViewModel>();
            Characters = Characterstemp.Where(m => m.RolePlayId == id).ToList();
            for (int i = 0; i < Characters.Count; i++)
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == Characters[i].UserId);
                CharactersViewModel.Add(new CharacterViewModel
                {
                    RolePlayId = id,
                    RolePlayName = RolePlay.Name,
                    UserId = Characters[i].UserId,
                    UserName = user.UserName,
                    СharacterName = Characters[i].СharacterName
                });

            }
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await GetLists((int)id);

            if (RolePlaysViewModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
