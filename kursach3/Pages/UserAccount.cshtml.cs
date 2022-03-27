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

namespace kursach3.Pages
{
    public class UserAccountModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly kursach3.Data.ApplicationDbContext _context;

        public UserAccountModel(kursach3.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [TempData]
        public string StatusMessage { get; set; }
        public static ApplicationUser UserFriend { get; set; }
        public UserAccountViewModel UserAccountViewModel { get; set; }
        public static Friend Friend { get; set; }
        public static bool FriendExist { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserFriend = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            UserAccountViewModel = new UserAccountViewModel { Avatar = UserFriend.Avatar, Bio = UserFriend.Bio, Id = UserFriend.Id, UserName = UserFriend.UserName };
            Friend = await _context.Friends.FirstOrDefaultAsync(m => m.UserId == id || m.UserFriendId == id);
            if (Friend == null)
            {
                FriendExist = false;
            }
            else
            {
                FriendExist = true;
            }

            if (UserAccountViewModel == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!FriendExist)
            {
                Friend = new Friend { User = user, UserId = user.Id, UserFriend = UserFriend, UserFriendId = UserFriend.Id, Timestamp = DateTime.Now, Confirmed = false };
                _context.Entry(Friend.UserFriend).State = EntityState.Unchanged;
                _context.Friends.Add(Friend);
                await _context.SaveChangesAsync();
                StatusMessage = "You have added a new friend!";
                return Redirect("/Friends");
            }
            else
            {
                Friend.Confirmed = true;
                Friend.Timestamp = DateTime.Now;
                _context.Attach(Friend).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                StatusMessage = "You have confirmed adding a new friend!";
                return Redirect("/Friends");
            }           
        }
    }
}
