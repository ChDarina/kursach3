using System;

namespace kursach3.ViewModels
{
    public class FriendViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
        public DateTime? Timestamp { get; set; }
        public bool Confirmed { get; set; }
    }
}
