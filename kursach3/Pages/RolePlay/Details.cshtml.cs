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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace kursach3.Pages.RolePlay
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly kursach3.Data.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public DetailsModel(kursach3.Data.ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public string UsernameSort { get; set; }
        public string CharacterNameSort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public static string RolePlayName { get; set; }
        public static string RolePlayMasterName { get; set; }
        public RolePlaysViewModel RolePlaysViewModel { get; set; }
        public Models.RolePlay RolePlay { get; set; }
        public Models.ApplicationUser User { get; set; }
        public List<CharacterViewModel> CharactersViewModel { get; set; }
        public IList<kursach3.Models.Character> Characters { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            if (id == null)
            {
                return NotFound();
            }

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
            var CharactersViewModel_temp = new List<CharacterViewModel>();
            Characters = Characterstemp.Where(m => m.RolePlayId == id).ToList();
            for (int i = 0; i < Characters.Count; i++)
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == Characters[i].UserId);
                CharactersViewModel_temp.Add(new CharacterViewModel
                {
                    RolePlayId = id,
                    RolePlayName = RolePlay.Name,
                    UserId = Characters[i].UserId,
                    UserName = user.UserName,
                    СharacterName = Characters[i].СharacterName
                });
            }
            CharactersViewModel = CharactersViewModel_temp;

            //CurrentSort = sortOrder;
            //UsernameSort = String.IsNullOrEmpty(sortOrder) ? "Username" : "Username_desc";
            //CharacterNameSort = String.IsNullOrEmpty(sortOrder) ? "CharacterName" : "CharacterName_desc";

            //if (searchString != null)
            //{
            //    pageIndex = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            //CurrentFilter = searchString;


            //IEnumerable<CharacterViewModel> characterIQ = from s in CharactersViewModel_temp
            //                                              select s;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    characterIQ = characterIQ.Where(s => s.UserName.Contains(searchString)
            //                           || s.СharacterName.Contains(searchString));
            //}

            //switch (sortOrder)
            //{
            //    case "Username":
            //        characterIQ = characterIQ.OrderBy(s => s.UserName);
            //        break;
            //    case "Username_desc":
            //        characterIQ = characterIQ.OrderByDescending(s => s.UserName);
            //        break;
            //    case "CharacterName":
            //        characterIQ = characterIQ.OrderBy(s => s.СharacterName);
            //        break;
            //    case "CharacterName_desc":
            //        characterIQ = characterIQ.OrderByDescending(s => s.СharacterName);
            //        break;
            //    default:
            //        characterIQ = characterIQ.OrderBy(s => s.UserName);
            //        break;
            //}
            //var pageSize = _configuration.GetValue("PageSize", 10);
            //CharactersViewModel = PaginatedList<CharacterViewModel>.Create(
            //    characterIQ, pageIndex ?? 1, pageSize);

            if (RolePlaysViewModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
