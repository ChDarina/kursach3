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
using kursach3.Models;
using Microsoft.AspNetCore.Identity;

namespace kursach3.Pages
{
    public class FriendsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly kursach3.Data.ApplicationDbContext _context;

        public FriendsModel(kursach3.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IList<FriendViewModel> FriendViewModel { get;set; }
        public IList<FriendViewModel> FriendViewModelTemp { get; set; }
        public IList<kursach3.Models.ApplicationUser> Users { get; set; }
        public IList<kursach3.Models.Friend> Friends { get; set; }

        public async Task OnGetAsync()
        {
            FriendViewModel = await FriendsGetAsync();
        }
        private async Task<IList<FriendViewModel>> FriendsGetAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            Friends = await _context.Friends.ToListAsync();
            FriendViewModelTemp = new List<FriendViewModel>();
            for (int i = 0; i < Friends.Count; i++)
            {
                if (user.Id == Friends[i].UserId)
                {
                    var friendid = Friends[i].UserFriendId;
                    var friend = await _context.Users.FirstOrDefaultAsync(m => m.Id == friendid);
                    var item = new FriendViewModel { Id = friendid, Avatar = friend.Avatar, UserName = friend.UserName, Bio = friend.Bio, Confirmed = Friends[i].Confirmed, Timestamp = Friends[i].Timestamp };
                    FriendViewModelTemp.Add(item);
                }
                if (user.Id == Friends[i].UserFriendId)
                {
                    var friendid = Friends[i].UserId;
                    var friend = await _context.Users.FirstOrDefaultAsync(m => m.Id == friendid);
                    var item = new FriendViewModel { Id = friendid, Avatar = friend.Avatar, UserName = friend.UserName, Bio = friend.Bio, Confirmed = Friends[i].Confirmed, Timestamp = Friends[i].Timestamp };
                    FriendViewModelTemp.Add(item);
                }
            }
            return FriendViewModelTemp;
        }
    }
}
