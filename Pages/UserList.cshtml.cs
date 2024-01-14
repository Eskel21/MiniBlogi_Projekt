using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogiv2.Data.Models;
using MiniBlogiv2.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace MiniBlogiv2.Pages
{
    public class UserListModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserListModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public List<ApplicationUser> Users { get; set; }
     

        public IActionResult OnGet()
        {
            Users = _context.Users.ToList();
            return Page();
        }
    }
}
