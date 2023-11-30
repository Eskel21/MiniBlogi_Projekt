using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogi_Projekt.Data;
using MiniBlogi_Projekt.Data.Models;

namespace MiniBlogi_Projekt.Pages
{
    public class ClipArtyModel_Logout : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ClipArtyModel_Logout(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        public List<Image> Images { get; set; }

        public void OnGet()
        {
            Images = _dbContext.Image.ToList();
        }
       
    }
}
