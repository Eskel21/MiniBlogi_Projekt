﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniBlogiv2.Data;
using MiniBlogiv2.Data.Models;

namespace MiniBlogiv2.Pages.Notes
{
    public class EditModel : PageModel
    {
        private readonly MiniBlogiv2.Data.ApplicationDbContext _context;

        public EditModel(MiniBlogiv2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Note Note { get; set; } = default!;
        [BindProperty]
        public List<int> SelectedTags { get; set; }

        public List<Tag> AllTags { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            AllTags = _context.Tag.ToList();
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note =  await _context.Note.FirstOrDefaultAsync(m => m.NoteId == id);
            if (note == null)
            {
                return NotFound();
            }
            Note = note;
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

       
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                
                var existingTagNotes = _context.TagNote.Where(tn => tn.NoteId == Note.NoteId);
                _context.TagNote.RemoveRange(existingTagNotes);
                await _context.SaveChangesAsync();

                
                if (SelectedTags != null && SelectedTags.Any())
                {
                    foreach (var tagId in SelectedTags)
                    {
                        var tagNote = new TagNote { NoteId = Note.NoteId, TagId = tagId };
                        _context.TagNote.Add(tagNote);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(Note.NoteId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }


        private bool NoteExists(int id)
        {
          return (_context.Note?.Any(e => e.NoteId == id)).GetValueOrDefault();
        }
    }
}
