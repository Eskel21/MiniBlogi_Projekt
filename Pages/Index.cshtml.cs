using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System;

using MiniBlogiv2.Data.Models;
using MiniBlogiv2.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace MiniBlogiv2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        [BindProperty]
        public Comment NewComment { get; set; }
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<Note> LatestNotes { get; set; }
        public List<Comment> Comments { get; set; }
        public IActionResult OnGet()
        {
            ViewData["NoteId"] = new SelectList(_context.Note, "NoteId", "NoteId");
            

            LatestNotes = _context.Note
                .Include(n => n.Comment)
                .OrderByDescending(n => n.CreatedAt)
                .Take(10)
                .ToList();
            return Page();
        }
        [Authorize]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid || _context.Comment == null || NewComment == null)
            {
                return Page();
            }

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

       

    }
}