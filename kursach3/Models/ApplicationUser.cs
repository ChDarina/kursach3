using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kursach3.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            RolePlays = new HashSet<RolePlay>();
            Rooms = new HashSet<Room>();
            UserFeed = new HashSet<Feed>();
            Messages = new HashSet<Message>();
            Characters = new HashSet<Character>();
            SocialMediaUsers = new HashSet<SocialMediaUser>();
        }
        [Required]
        public string Avatar { get; set; }
        public string? Bio { get; set; }
        public virtual ICollection<RolePlay> RolePlays { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Feed> UserFeed { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<SocialMediaUser> SocialMediaUsers { get; set; }
    }
}
