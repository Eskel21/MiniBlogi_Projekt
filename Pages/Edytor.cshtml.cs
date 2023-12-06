using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogiv2.Data.Models;
using MiniBlogiv2.Data;
using MiniBlogiv2;
using Microsoft.EntityFrameworkCore;


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
       AllTags=_context.Tag.ToList();

    }

    public async Task<IActionResult> OnPostAsync()
    {

        if (ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Wyst¹pi³ b³¹d walidacji.");
            return Page();
        }

        
        ApplicationUser user = await _userManager.GetUserAsync(User);
        
        Note.UserId = user.Id;

        
        _context.Note.Add(Note);
        if (SelectedTags != null && SelectedTags.Any())
        {
            foreach (var tagId in SelectedTags)
            {
                var tagNote = new TagNote { NoteId = Note.NoteId, TagId = tagId };
                _context.TagNote.Add(tagNote);
            }

            await _context.SaveChangesAsync();
        }

        return RedirectToPage("/Index"); 
    }
}