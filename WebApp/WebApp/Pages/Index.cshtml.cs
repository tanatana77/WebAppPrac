using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.DataAccess.WebAppDbContext _context;

        public IndexModel(WebApp.DataAccess.WebAppDbContext context)
        {
            _context = context;
        }

        public IList<UUser> UUser { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var targetUsers = new long[] { 1, 2, 3 };

            UUser = await _context.UUsers
                .Where(u => targetUsers.Contains(u.Id))
                .ToArrayAsync();

            UUser = await _context.UUsers.ToListAsync();
        }
    }
}
