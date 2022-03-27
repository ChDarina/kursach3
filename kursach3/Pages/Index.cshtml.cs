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
using Microsoft.AspNetCore.Identity;
using kursach3.ViewModels;

namespace kursach3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly kursach3.Data.ApplicationDbContext _context;

        public IndexModel(kursach3.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IList<kursach3.ViewModels.FeedViewModel> FeedView { get;set; }
        public IList<kursach3.ViewModels.FeedViewModel> FeedViewTemp { get; set; }
        public IList<kursach3.Models.Feed> Feed { get; set; }
        public IList<kursach3.Models.ApplicationUser> Users { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            var feed = await FeedGetAsync();
            //IQueryable<FeedViewModel> feedIQ = (IQueryable<FeedViewModel>)(from s in feed
            //                                       select s);

            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        feedIQ = feedIQ.OrderByDescending(s => s.Title);
            //        break;
            //    case "Date":
            //        feedIQ = feedIQ.OrderBy(s => s.Timestamp);
            //        break;
            //    case "date_desc":
            //        feedIQ = feedIQ.OrderByDescending(s => s.Timestamp);
            //        break;
            //    default:
            //        feedIQ = feedIQ.OrderBy(s => s.Content);
            //        break;
            //}
            //FeedView = await feedIQ.AsNoTracking().ToListAsync();
            FeedView = feed;
        }
        private async Task<IList<FeedViewModel>> FeedGetAsync()
        {
            Users = await _context.Users.ToListAsync();
            Feed = await _context.Feed.ToListAsync();
            FeedViewTemp = new List<FeedViewModel>();
            for (int i = 0; i < Feed.Count; i++)
            {
                var item = new FeedViewModel { Id = Feed[i].Id, Title = Feed[i].Title, Avatar = Feed[i].User.Avatar, Content = Feed[i].Content, Timestamp = (DateTime)Feed[i].Timestamp, UserName = Feed[i].User.UserName, UserId = Feed[i].User.Id };
                FeedViewTemp.Add(item);
            }
            return FeedViewTemp;
        }

    }
}
