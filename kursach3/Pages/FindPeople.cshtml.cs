#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using kursach3.Data;
using kursach3.Models;
using Microsoft.AspNetCore.Identity;
using kursach3.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace kursach3.Pages
{
    [Authorize]
    public class FindPeopleModel : PageModel
    {
        private readonly kursach3.Data.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public FindPeopleModel(kursach3.Data.ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public string UserNameSort { get; set; }
        public string BioSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<AppUserViewModel> AppUserView { get;set; }
        public List<AppUserViewModel> AppUserViewTemp { get; set; }
        public List<kursach3.Models.ApplicationUser> Users { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            UserNameSort = String.IsNullOrEmpty(sortOrder) ? "UserName" : "UserName_desc";
            BioSort = String.IsNullOrEmpty(sortOrder) ? "Bio" : "Bio_desc";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            var users = await UsersGetAsync();

            IEnumerable<AppUserViewModel> userIQ = from s in users
                                                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                userIQ = userIQ.Where(s => s.UserName.Contains(searchString)
                                       || s.Bio.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "UserName":
                    userIQ = userIQ.OrderBy(s => s.UserName);
                    break;
                case "UserName_desc":
                    userIQ = userIQ.OrderByDescending(s => s.UserName);
                    break;
                case "Bio":
                    userIQ = userIQ.OrderBy(s => s.Bio);
                    break;
                case "Bio_desc":
                    userIQ = userIQ.OrderByDescending(s => s.Bio);
                    break;
                default:
                    userIQ = userIQ.OrderByDescending(s => s.UserName);
                    break;
            }
            var pageSize = _configuration.GetValue("PageSize", 10);
            AppUserView = PaginatedList<AppUserViewModel>.Create(
                userIQ, pageIndex ?? 1, pageSize);
        }
        private async Task<List<AppUserViewModel>> UsersGetAsync()
        {
            Users = await _context.Users.ToListAsync();
            AppUserViewTemp = new List<AppUserViewModel>();
            for (int i = 0; i < Users.Count; i++)
            {
                var item = new AppUserViewModel { Id = Users[i].Id, Avatar = Users[i].Avatar, UserName = Users[i].UserName, Bio = Users[i].Bio };
                AppUserViewTemp.Add(item);
            }
            return AppUserViewTemp;
        }
    }
}
