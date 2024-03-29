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
        public List<Tag> Tags { get; set; }
        public List<MiniBlogiv2.Data.Models.TagNote> tagNotes { get; set; }
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

            Tags = _context.Tag.ToList();
            tagNotes = _context.TagNote.ToList();

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
        [Authorize]
        public IActionResult OnPostLike(int noteId)
        {
            var note = _context.Note.FirstOrDefault(n => n.NoteId == noteId);

            if (note != null)
            {
                var loggedInUserName = User.Identity.Name;


                if (note.UsersLiked == null)
                {
                    note.UsersLiked = new string[] { loggedInUserName };
                }
                else if (note.UsersLiked.Contains(loggedInUserName))
                {

                    note.UsersLiked = note.UsersLiked.Where(u => u != loggedInUserName).ToArray();
                }
                else
                {

                    note.UsersLiked = note.UsersLiked.Append(loggedInUserName).ToArray();
                }

                _context.SaveChanges();
            }


            return RedirectToPage("./Index");
        }
    }
}
