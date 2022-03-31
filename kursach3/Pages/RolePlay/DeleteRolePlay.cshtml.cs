using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kursach3.Data;
using kursach3.Models;

namespace kursach3.Pages.RolePlay
{
    public class DeleteRolePlayModel : PageModel
    {
        private readonly kursach3.Data.ApplicationDbContext _context;

        public DeleteRolePlayModel(kursach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.RolePlay RolePlay { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RolePlay = await _context.RolePlays
                .Include(r => r.Master).FirstOrDefaultAsync(m => m.RolePlayId == id);


            if (RolePlay == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RolePlay = await _context.RolePlays.FindAsync(id);
            var characters = _context.Characters.Where(m => m.RolePlayId == RolePlay.RolePlayId);
            var rooms = _context.Rooms.Where(m => m.RolePlayId == RolePlay.RolePlayId);
            var messages = _context.Messages.Where(m => m.ToRoom.RolePlayId == RolePlay.RolePlayId);

            if (RolePlay != null)
            {
                _context.Messages.RemoveRange(messages);
                _context.Rooms.RemoveRange(rooms);
                _context.Characters.RemoveRange(characters);
                _context.RolePlays.Remove(RolePlay);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
