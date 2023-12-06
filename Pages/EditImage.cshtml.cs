using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniBlogiv2.Pages
{
    
        public class ProfileModel : PageModel
        {
            private readonly UserManager<ApplicationUser> _userManager;


            [BindProperty]
            public IFormFile Picture { get; set; }

            public ProfileModel(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;

            }

            public async Task<IActionResult> OnPostAsync()
            {
                if (Picture != null && Picture.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Picture.CopyToAsync(memoryStream);
                        var user = await _userManager.GetUserAsync(User);
                        user.Picture = memoryStream.ToArray();
                        await _userManager.UpdateAsync(user);
                    }
                }

                return RedirectToPage("/EditImage");
            }
        }
    
}
