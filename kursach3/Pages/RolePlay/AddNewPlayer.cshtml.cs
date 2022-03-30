#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kursach3.Data;
using kursach3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace kursach3.Pages.RolePlay
{
    [Authorize]
    public class AddNewPlayerModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly kursach3.Data.ApplicationDbContext _context;

        public AddNewPlayerModel(kursach3.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public string ErrorMessage { get; set; }
        public IActionResult OnGet(int id)
        {
            var userId = _userManager.GetUserId(User);
            var roleplay = _context.RolePlays.FirstOrDefault(m => m.RolePlayId == id);
            var friends = _context.Friends.Where(m => m.UserId == userId || m.UserFriendId == userId);
            var characters = _context.Characters.Where(m => m.RolePlayId == id);
            
            List<ApplicationUser> charactersUsers = new List<ApplicationUser>();
            foreach (var character in characters)
            {
                if (character.UserId != userId) charactersUsers.Add(_context.ApplicationUsers.FirstOrDefault(m => m.Id == character.UserId));
            }

            List<ApplicationUser> usersFriends = new List<ApplicationUser>();
            foreach (var friend in friends)
            {
                if (userId == friend.UserId)
                {
                    var usertempid = _context.ApplicationUsers.FirstOrDefault(m => m.Id == friend.UserFriendId);
                    usersFriends.Add(usertempid);
                }
                if(userId == friend.UserFriendId)
                {
                    var usertempid = _context.ApplicationUsers.FirstOrDefault(m => m.Id == friend.UserId);
                    usersFriends.Add(usertempid);
                }
            }

            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach(var user in usersFriends)
            {
                bool found = false;
                foreach (var character in charactersUsers)
                {
                    if (user.Id == character.Id) found = true;
                }
                if (!found) users.Add(user);
            }
            if (users.Count != 0)
            {
                _roleplay = roleplay;
                ViewData["UserId"] = new SelectList(users, "Id", "UserName");
                return Page();
            }
            ErrorMessage = String.Format("No friends awailable to add!");
            return RedirectToPage("./Index");
        }
        public Models.RolePlay _roleplay { get; set; }

        [BindProperty]
        public Character Character { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Characters.Add(Character);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
