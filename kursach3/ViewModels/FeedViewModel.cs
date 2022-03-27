using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3.ViewModels
{
    public class FeedViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Avatar { get; set; }
    }
}
