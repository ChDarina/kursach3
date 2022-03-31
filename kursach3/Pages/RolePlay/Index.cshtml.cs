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
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace kursach3.Pages.RolePlay
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly kursach3.Data.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(kursach3.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }
        public string NameSort { get; set; }
        public string DescriptionSort { get; set; }
        public string MasterUsernameSort { get; set; }
        public string CharacterNameSort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<RolePlaysViewModel> RolePlaysView { get;set; }
        public IList<RolePlaysViewModel> RolePlaysViewTemp { get; set; }
        public IList<kursach3.Models.ApplicationUser> Users { get; set; }
        public IList<kursach3.Models.Character> Characters { get; set; }
        public IList<kursach3.Models.RolePlay> RolePlays { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "Name" : "Name_desc";
            DescriptionSort = String.IsNullOrEmpty(sortOrder) ? "Description" : "Description_desc";
            MasterUsernameSort = String.IsNullOrEmpty(sortOrder) ? "MasterUsername" : "MasterUsername_desc";
            CharacterNameSort = String.IsNullOrEmpty(sortOrder) ? "CharacterName" : "CharacterName_desc";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            var roleplay = await RolePlayGetAsync();

            IEnumerable<RolePlaysViewModel> roleplayIQ = from s in roleplay
                                                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                roleplayIQ = roleplayIQ.Where(s => s.Name.Contains(searchString)
                                       || s.MasterUsername.Contains(searchString)
                                       || s.Description.Contains(searchString)
                                       || s.CharacterName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Name":
                    roleplayIQ = roleplayIQ.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    roleplayIQ = roleplayIQ.OrderByDescending(s => s.Name);
                    break;
                case "MasterUsername":
                    roleplayIQ = roleplayIQ.OrderBy(s => s.MasterUsername);
                    break;
                case "MasterUsername_desc":
                    roleplayIQ = roleplayIQ.OrderByDescending(s => s.MasterUsername);
                    break;
                case "Description":
                    roleplayIQ = roleplayIQ.OrderBy(s => s.Description);
                    break;
                case "Description_desc":
                    roleplayIQ = roleplayIQ.OrderByDescending(s => s.Description);
                    break;
                case "CharacterName":
                    roleplayIQ = roleplayIQ.OrderBy(s => s.CharacterName);
                    break;
                case "CharacterName_desc":
                    roleplayIQ = roleplayIQ.OrderByDescending(s => s.CharacterName);
                    break;
                default:
                    roleplayIQ = roleplayIQ.OrderBy(s => s.Name);
                    break;
            }
            var pageSize = _configuration.GetValue("PageSize", 10);
            RolePlaysView = PaginatedList<RolePlaysViewModel>.Create(
                roleplayIQ, pageIndex ?? 1, pageSize);
        }
        private async Task<IList<RolePlaysViewModel>> RolePlayGetAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            RolePlays = await _context.RolePlays.ToListAsync();
            Characters = await _context.Characters.ToListAsync();
            RolePlaysViewTemp = new List<RolePlaysViewModel>();
            for (int i = 0; i < Characters.Count; i++)
            {
                if (user.Id == Characters[i].UserId)
                {
                    var roleplay = Characters[i].RolePlay;
                    var masteruser = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == roleplay.MasterId);
                    var item = new RolePlaysViewModel { Id = roleplay.RolePlayId, MasterId = roleplay.MasterId, Description = roleplay.Description, MasterUsername = masteruser.UserName, Name = roleplay.Name, CharacterName = Characters[i].СharacterName};
                    RolePlaysViewTemp.Add(item);
                }
            }
            return RolePlaysViewTemp;
        }
    }
}
