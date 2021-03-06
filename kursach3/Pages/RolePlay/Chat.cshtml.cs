using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace kursach3.Pages
{
    [Authorize]
    public class ChatModel : PageModel
    {
        private readonly ILogger<ChatModel> _logger;
        public int chat_id;

        public ChatModel(ILogger<ChatModel> logger)
        {
            _logger = logger;

        }

        public void OnGet(int id)
        {
            chat_id = id;
        }
    }
}
