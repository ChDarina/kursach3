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
using kursach3.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace kursach3.Pages.RolePlay
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly kursach3.Data.ApplicationDbContext _context;

        public EditModel(kursach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Models.RolePlay RolePlay { get; set; }
        public int RolePlayId { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RolePlayId = (int)id;
            RolePlay = await _context.RolePlays.Include("Master").FirstOrDefaultAsync(m => m.RolePlayId == id);

            if (RolePlay == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (RolePlay != null)
            {
                _context.Entry(RolePlay).State = EntityState.Detached;
            }
            _context.Attach(RolePlay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolePlaysViewModelExists(RolePlay.RolePlayId))
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

        private bool RolePlaysViewModelExists(int id)
        {
            return _context.RolePlays.Any(e => e.RolePlayId == id);
        }
    }
}
