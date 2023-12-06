using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniBlogiv2.Data;
using MiniBlogiv2.Data.Models;

namespace MiniBlogiv2.Pages.Notes
{
    public class IndexModel : PageModel
    {
        private readonly MiniBlogiv2.Data.ApplicationDbContext _context;
        public List<Tag> Tags { get; set; }
        public List<TagNote> tagNotes { get; set; }
        public IndexModel(MiniBlogiv2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Note> Note { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Note != null)
            {
                Note = await _context.Note
                .Include(n => n.User).ToListAsync();
            }
            Tags = _context.Tag.ToList();
            tagNotes = _context.TagNote.ToList();
        }
    }
}
