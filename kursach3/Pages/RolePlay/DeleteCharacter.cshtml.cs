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
    public class DeleteCharacterModel : PageModel
    {
        private readonly kursach3.Data.ApplicationDbContext _context;

        public DeleteCharacterModel(kursach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Character Character { get; set; }

        public async Task<IActionResult> OnGetAsync(int? roleplayid, string userid)
        {
            if (roleplayid == null || userid == null)
            {
                return NotFound();
            }

            Character = await _context.Characters
                .Include(c => c.RolePlay).Where(m => m.RolePlayId == roleplayid)
                .Include(c => c.User).FirstOrDefaultAsync(m => m.UserId == userid);

            if (Character == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? roleplayid, string userid)
        {
            if (roleplayid == null || userid == null)
            {
                return NotFound();
            }

            Character = await _context.Characters
                .Include(c => c.RolePlay).Where(m => m.RolePlayId == roleplayid)
                .Include(c => c.User).FirstOrDefaultAsync(m => m.UserId == userid);

            if (Character != null)
            {
                _context.Characters.Remove(Character);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
