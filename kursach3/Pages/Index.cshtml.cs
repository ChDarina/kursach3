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
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly kursach3.Data.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(kursach3.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }
        public string UserNameSort { get; set; }
        public string TitleSort { get; set; }
        public string ContentSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public List<kursach3.ViewModels.FeedViewModel> FeedViewTemp { get; set; }
        public List<kursach3.Models.Feed> Feed { get; set; }
        public List<kursach3.Models.ApplicationUser> Users { get; set; }
        public PaginatedList<FeedViewModel> FeedView { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            UserNameSort = String.IsNullOrEmpty(sortOrder) ? "UserName" : "UserName_desc";
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "Title" : "Title_desc";
            ContentSort = String.IsNullOrEmpty(sortOrder) ? "Content" : "Content_desc";
            DateSort = sortOrder == "Date" ? "Date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            var feed = await FeedGetAsync();

            IEnumerable<FeedViewModel> feedIQ = from s in feed
                                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                feedIQ = feedIQ.Where(s => s.UserName.Contains(searchString)
                                       || s.Title.Contains(searchString) 
                                       || s.Content.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "UserName":
                    feedIQ = feedIQ.OrderBy(s => s.UserName);
                    break;
                case "UserName_desc":
                    feedIQ = feedIQ.OrderByDescending(s => s.UserName);
                    break;
                case "Title":
                    feedIQ = feedIQ.OrderBy(s => s.Title);
                    break;
                case "Titled_desc":
                    feedIQ = feedIQ.OrderByDescending(s => s.Title);
                    break;
                case "Content":
                    feedIQ = feedIQ.OrderBy(s => s.Content);
                    break;
                case "Content_desc":
                    feedIQ = feedIQ.OrderByDescending(s => s.Content);
                    break;
                case "Date":
                    feedIQ = feedIQ.OrderBy(s => s.Timestamp);
                    break;
                case "Date_desc":
                    feedIQ = feedIQ.OrderByDescending(s => s.Timestamp);
                    break;
                default:
                    feedIQ = feedIQ.OrderByDescending(s => s.Timestamp);
                    break;
            }
            var pageSize = _configuration.GetValue("PageSize", 10);
            FeedView = PaginatedList<FeedViewModel>.Create(
                feedIQ, pageIndex ?? 1, pageSize);
        }
        private async Task<List<FeedViewModel>> FeedGetAsync()
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
