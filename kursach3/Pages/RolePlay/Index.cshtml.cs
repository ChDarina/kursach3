#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kursach3.Data;
using kursach3.ViewModels;
using Microsoft.AspNetCore.Identity;
using kursach3.Models;

namespace kursach3.Pages.RolePlay
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly kursach3.Data.ApplicationDbContext _context;

        public IndexModel(kursach3.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<RolePlaysViewModel> RolePlaysViewModel { get;set; }
        public IList<RolePlaysViewModel> RolePlaysViewModelTemp { get; set; }
        public IList<kursach3.Models.ApplicationUser> Users { get; set; }
        public IList<kursach3.Models.Character> Characters { get; set; }
        public IList<kursach3.Models.RolePlay> RolePlays { get; set; }

        public async Task OnGetAsync()
        {
            RolePlaysViewModel = await RolePlayGetAsync();
        }
        private async Task<IList<RolePlaysViewModel>> RolePlayGetAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            RolePlays = await _context.RolePlays.ToListAsync();
            Characters = await _context.Characters.ToListAsync();
            RolePlaysViewModelTemp = new List<RolePlaysViewModel>();
            for (int i = 0; i < Characters.Count; i++)
            {
                if (user.Id == Characters[i].UserId)
                {
                    var roleplay = Characters[i].RolePlay;
                    var masteruser = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == roleplay.MasterId);
                    var item = new RolePlaysViewModel { Id = roleplay.RolePlayId, MasterId = roleplay.MasterId, Description = roleplay.Description, MasterUsername = masteruser.UserName, Name = roleplay.Name, CharacterName = Characters[i].СharacterName};
                    RolePlaysViewModelTemp.Add(item);
                }
            }
            return RolePlaysViewModelTemp;
        }
    }
}
