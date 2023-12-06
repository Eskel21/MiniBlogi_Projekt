using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogiv2.Data.Models;
using MiniBlogiv2.Data;
using MiniBlogiv2;

namespace MiniBlogiv2.Pages.Notes;
public class EdytorModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    [BindProperty]
    public Note Note { get; set; }
    [BindProperty]
    public List<int> SelectedTags { get; set; }

    public List<Tag> AllTags { get; set; }
    public EdytorModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public void OnGet()
    {
        AllTags = _context.Tag.ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {

        if (ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Wystąpił błąd walidacji.");
            return Page();
        }

        // Get the current user
        ApplicationUser user = await _userManager.GetUserAsync(User);
        // Assign the user ID to the Note
        Note.UserId = user.Id;

        // Add the note to the database
        _context.Note.Add(Note);
        await _context.SaveChangesAsync();
        if (SelectedTags != null && SelectedTags.Any())
        {
            foreach (var tagId in SelectedTags)
            {
                var tagNote = new TagNote { NoteId = Note.NoteId, TagId = tagId };
                _context.TagNote.Add(tagNote);
            }

            await _context.SaveChangesAsync();
        }

        return RedirectToPage("/Index"); // Redirect to the desired page after creating a note
    }
}