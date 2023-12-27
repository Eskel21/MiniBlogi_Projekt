using MiniBlogiv2.Data.Models;
namespace MiniBlogiv2.Interfaces
{
    public interface CommentInterface
    {
        public IQueryable<Comment> GetComments(int noteId);
    }
}
