using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.DataAccess;
using WebApp.Models;

namespace WebApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WebApp.DataAccess.WebAppDbContext _context;

        public CreateModel(WebApp.DataAccess.WebAppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UUser UUser { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.UUsers.Add(UUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
