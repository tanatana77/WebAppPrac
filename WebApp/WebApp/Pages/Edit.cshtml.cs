using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.DataAccess;
using WebApp.Models;

namespace WebApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly WebApp.DataAccess.WebAppDbContext _context;

        public EditModel(WebApp.DataAccess.WebAppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UUser UUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uuser =  await _context.UUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (uuser == null)
            {
                return NotFound();
            }
            UUser = uuser;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UUserExists(UUser.Id))
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

        private bool UUserExists(long id)
        {
            return _context.UUsers.Any(e => e.Id == id);
        }
    }
}
