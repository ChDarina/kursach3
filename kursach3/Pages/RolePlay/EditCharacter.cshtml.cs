#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kursach3.Data;
using kursach3.Models;
using Microsoft.AspNetCore.Authorization;

namespace kursach3.Pages.RolePlay
{
    [Authorize]
    public class EditCharacterModel : PageModel
    {
        private readonly kursach3.Data.ApplicationDbContext _context;

        public EditCharacterModel(kursach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Character Character { get; set; }

        public async Task<IActionResult> OnGetAsync(int? roleplayid, string userid)
        {
            if (userid == null || roleplayid == null)
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
           ViewData["RolePlayId"] = new SelectList(_context.RolePlays, "RolePlayId", "Description");
           ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(Character.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CharacterExists(string id)
        {
            return _context.Characters.Any(e => e.UserId == id);
        }
    }
}
