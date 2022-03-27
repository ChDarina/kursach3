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

namespace kursach3.Pages
{
    public class FindPeopleModel : PageModel
    {
        private readonly kursach3.Data.ApplicationDbContext _context;

        public FindPeopleModel(kursach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IList<AppUserViewModel> AppUserViewModel { get;set; }
        public IList<AppUserViewModel> AppUserViewModelTemp { get; set; }
        public IList<kursach3.Models.ApplicationUser> Users { get; set; }

        public async Task OnGetAsync()
        {
            AppUserViewModel = await UsersGetAsync();
        }
        private async Task<IList<AppUserViewModel>> UsersGetAsync()
        {
            Users = await _context.Users.ToListAsync();
            AppUserViewModelTemp = new List<AppUserViewModel>();
            for (int i = 0; i < Users.Count; i++)
            {
                var item = new AppUserViewModel { Id = Users[i].Id, Avatar = Users[i].Avatar, UserName = Users[i].UserName, Bio = Users[i].Bio };
                AppUserViewModelTemp.Add(item);
            }
            return AppUserViewModelTemp;
        }
    }
}
