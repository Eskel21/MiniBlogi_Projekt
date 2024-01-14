using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogiv2.Data;

namespace MiniBlogiv2.Pages
{
    public class DeleteUsernModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteUsernModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public ApplicationUser UserToDelete { get; set; }

        public IActionResult OnGet()
        {
            UserToDelete = _context.Users.Find(Id);

            if (UserToDelete == null)
            {
                
                return Redirect("/UserList");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           
                var userToDelete =  await _context.Users.FindAsync(Id);
                if (userToDelete != null)
                {
                
                    _context.Users.Remove(userToDelete);
                    await _context.SaveChangesAsync();
            }

            
            return Redirect("./UserList");
        }
    }
}
