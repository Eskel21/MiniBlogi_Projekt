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
            Users =_context.Users.ToList();

            return Page();
        }

        public IActionResult OnPostDeleteUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            var userToDelete = _context.Users.FirstOrDefault(u => u.Id.Equals(userId));

            if (userToDelete == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userToDelete);
            _context.SaveChanges();

            return RedirectToPage("/UserList");
        }
}
}
