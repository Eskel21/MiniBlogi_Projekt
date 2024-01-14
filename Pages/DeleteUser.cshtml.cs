using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniBlogiv2.Data;
using MiniBlogiv2.Data.Models;
using System.Threading.Tasks;

namespace MiniBlogiv2.Pages.Users
{
    public class DeleteUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteUserModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ApplicationUser UserToDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            UserToDelete = await _context.Users.FirstOrDefaultAsync(m => m.Id == userId);

            if (UserToDelete == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            UserToDelete = await _context.Users.FindAsync(userId);

            if (UserToDelete != null)
            {
                _context.Users.Remove(UserToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}