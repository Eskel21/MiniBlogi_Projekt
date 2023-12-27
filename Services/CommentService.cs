using MiniBlogiv2.Interfaces;
using MiniBlogiv2.Data.Models;
using System;
using MiniBlogiv2.Data;

namespace MiniBlogiv2.Services
{
    public class CommentService : CommentInterface
    {
        private readonly ApplicationDbContext _context;
        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Comment> GetComments(int noteId)
        {
            return _context.Comment.Where(c => c.NoteId == noteId);
        }
    }

}
