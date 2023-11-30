using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogi_Projekt.Data;
using MiniBlogi_Projekt.Data.Models;

namespace MiniBlogi_Projekt.Pages
{
    public class ClipArtyModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ClipArtyModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        public List<Image> Images { get; set; }

        public void OnGet()
        {
            Images = _dbContext.Image.ToList();
        }
        public IActionResult OnPost(string title, IFormFile image)
        {
            // Obs³u¿ przes³ane obrazy i zapisz je do bazy danych
            if (image != null && image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var imageData = ms.ToArray();

                    var newImage = new Image
                    {
                        Title = title,
                        ImageData = imageData
                    };

                    _dbContext.Image.Add(newImage);
                    _dbContext.SaveChanges();
                }
            }

            // Przekieruj na stronê ClipArty po dodaniu obrazu
            return RedirectToPage("/ClipArty");
        }
    }
}
