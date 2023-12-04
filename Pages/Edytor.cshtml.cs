using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogiv2.Data.Models;
using MiniBlogiv2.Data;
using MiniBlogiv2;

public class EdytorModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    [BindProperty]
    public Note Note { get; set; }

    public EdytorModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public void OnGet()
    {
        // Initialization logic if needed
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Get the current user
        ApplicationUser user = await _userManager.GetUserAsync(User);

        // Assign the user ID to the Note
        Note.UserId = user.Id;

        // Set any other properties or logic you need

        // Add the note to the database
        _context.Note.Add(Note);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Index"); // Redirect to the desired page after creating a note
    }
}