using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.DataAccess;
using WebApp.Models;

namespace WebApp.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly WebApp.DataAccess.WebAppDbContext _context;

        public DeleteModel(WebApp.DataAccess.WebAppDbContext context)
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

            var uuser = await _context.UUsers.FirstOrDefaultAsync(m => m.Id == id);

            if (uuser is not null)
            {
                UUser = uuser;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uuser = await _context.UUsers.FindAsync(id);
            if (uuser != null)
            {
                UUser = uuser;
                _context.UUsers.Remove(UUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
