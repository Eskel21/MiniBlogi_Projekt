using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogiv2.Data.Models;
using MiniBlogiv2.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MiniBlogiv2.Pages
{
    public class UserPostsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserPostsModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public Comment NewComment { get; set; }
        public string UserName { get; set; }
        public List<Note> UserNotes { get; set; }
        public int totalNotes { get; set; }
        public int pageNo { get; set; }
        public int pageSize { get; set; }

        public IActionResult OnGet(string username, int p = 1, int s = 10)
        {
            UserName = username;
            
            UserNotes = _context.Note
            .Where(n => n.User.UserName == UserName)
            .OrderByDescending(n => n.CreatedAt)
            .Include(n => n.User)
            .Include(n => n.Comment)
            .Skip((p - 1) * s).Take(s)
            .ToList();
            pageSize = s;
            totalNotes = _context.Note
            .Where(n => n.User.UserName == UserName).Count();
            pageNo = p;
            
           

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid || _context.Comment == null || NewComment == null)
            {
                return Page();
            }
            NewComment.UserId = User.Identity.Name;
            _context.Comment.Add(NewComment);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
        
        public string GetIpAddress()
        {
            var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;

            if (remoteIpAddress != null)
            {


                remoteIpAddress = remoteIpAddress.MapToIPv4();


                return remoteIpAddress.ToString();
            }


            return "Unknown";
        }
        public async Task<IActionResult> OnPostDeleteCommentAsync(int commentId)
        {
            var commentToDelete = await _context.Comment.FindAsync(commentId);
            var note = _context.Note.FirstOrDefault(n => n.NoteId == commentToDelete.NoteId);
            var owner = _context.Users.FirstOrDefault(u => u.Id == note.UserId);

            if (commentToDelete != null && (User.Identity.Name == commentToDelete.UserId || User.Identity.Name == owner.UserName))
            {
                _context.Comment.Remove(commentToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
