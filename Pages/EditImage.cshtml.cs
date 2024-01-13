using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using MiniBlogiv2;
using MiniBlogiv2.Data;
using System.IO;
using System.Threading.Tasks;

public class ProfileModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public IFormFile Picture { get; set; }

    public ProfileModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    [BindProperty]
    public string CanvasData { get; set; }

    public async Task<IActionResult> OnPostSaveImageAsync()
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null && !string.IsNullOrEmpty(CanvasData))
            {
                
                user.Picture = Convert.FromBase64String(CanvasData.Split(',')[1]);

                await _userManager.UpdateAsync(user);

               _context.SaveChanges();

                return Redirect("/Identity/Account/Manage/Index");
            }
        }

        return Page();
    }
}
