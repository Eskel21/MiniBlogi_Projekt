using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System;

using MiniBlogiv2.Data.Models;
using MiniBlogiv2.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using MiniBlogiv2.Interfaces;
using System.Security.Claims;
using MiniBlogiv2.Data.Migrations;

namespace MiniBlogiv2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CommentInterface _commentService;
        
        [BindProperty]
        public Comment NewComment { get; set; }
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, CommentInterface commentService)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _commentService = commentService;
        }
        public List<Note> LatestNotes { get; set; }
        public List<Note> Notes { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Tag> Tags { get; set; }
        public List<MiniBlogiv2.Data.Models.TagNote> tagNotes { get; set; }
        public int totalNotes { get; set; }
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public IActionResult OnGet(int p=1, int s=10)
        {
            ViewData["NoteId"] = new SelectList(_context.Note, "NoteId", "NoteId");
            LatestNotes = _context.Note
            .OrderByDescending(n => n.CreatedAt)
            .Include(n => n.User)
            .Include(n => n.Comment)
            .Skip((p - 1) * s).Take(s)
            .ToList();
            pageSize = s;
            totalNotes = _context.Note.Count();
            pageNo = p;
            foreach (var note in LatestNotes)
            {
                note.Comment = _commentService.GetComments(note.NoteId).ToList();
            }
            Tags = _context.Tag.ToList();
            tagNotes = _context.TagNote.ToList();
            return Page();
        }
        [Authorize]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid || _context.Comment == null || NewComment == null)
            {
                return Page();
            }
            NewComment.UserId = User.Identity.Name;
            _context.Comment.Add(NewComment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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

            return RedirectToPage("./Index");
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