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
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace kursach3.Pages
{
    [Authorize]
    public class FriendsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly kursach3.Data.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public FriendsModel(kursach3.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }
        public string UserNameSort { get; set; }
        public string BioSort { get; set; }
        public string ConfirmedSort { get; set; }
        public string TimestampSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<FriendViewModel> FriendView { get;set; }
        public IList<FriendViewModel> FriendViewTemp { get; set; }
        public IList<kursach3.Models.ApplicationUser> Users { get; set; }
        public IList<kursach3.Models.Friend> Friends { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            UserNameSort = String.IsNullOrEmpty(sortOrder) ? "UserName" : "UserName_desc";
            BioSort = String.IsNullOrEmpty(sortOrder) ? "Bio" : "Bio_desc";
            ConfirmedSort = sortOrder == "Confirmed" ? "Confirmed" : "Confirmed_desc";
            TimestampSort = sortOrder == "Timestamp" ? "Timestamp_desc" : "Timestamp";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            var users = await FriendsGetAsync();

            IEnumerable<FriendViewModel> userIQ = from s in users
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
                case "Confirmed":
                    userIQ = userIQ.OrderBy(s => s.Confirmed);
                    break;
                case "Confirmed_desc":
                    userIQ = userIQ.OrderByDescending(s => s.Confirmed);
                    break;
                case "Timestamp":
                    userIQ = userIQ.OrderBy(s => s.Timestamp);
                    break;
                case "Timestamp_desc":
                    userIQ = userIQ.OrderByDescending(s => s.Timestamp);
                    break;
                default:
                    userIQ = userIQ.OrderByDescending(s => s.Timestamp);
                    break;
            }
            var pageSize = _configuration.GetValue("PageSize", 10);
            FriendView = PaginatedList<FriendViewModel>.Create(
                userIQ, pageIndex ?? 1, pageSize);
        }
        private async Task<IList<FriendViewModel>> FriendsGetAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            Friends = await _context.Friends.ToListAsync();
            FriendViewTemp = new List<FriendViewModel>();
            for (int i = 0; i < Friends.Count; i++)
            {
                if (user.Id == Friends[i].UserId)
                {
                    var friendid = Friends[i].UserFriendId;
                    var friend = await _context.Users.FirstOrDefaultAsync(m => m.Id == friendid);
                    var item = new FriendViewModel { Id = friendid, Avatar = friend.Avatar, UserName = friend.UserName, Bio = friend.Bio, Confirmed = Friends[i].Confirmed, Timestamp = Friends[i].Timestamp };
                    FriendViewTemp.Add(item);
                }
                if (user.Id == Friends[i].UserFriendId)
                {
                    var friendid = Friends[i].UserId;
                    var friend = await _context.Users.FirstOrDefaultAsync(m => m.Id == friendid);
                    var item = new FriendViewModel { Id = friendid, Avatar = friend.Avatar, UserName = friend.UserName, Bio = friend.Bio, Confirmed = Friends[i].Confirmed, Timestamp = Friends[i].Timestamp };
                    FriendViewTemp.Add(item);
                }
            }
            return FriendViewTemp;
        }
    }
}
