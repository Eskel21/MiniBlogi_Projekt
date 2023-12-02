using Microsoft.AspNetCore.Identity;
using MiniBlogiv2.Data.Models;

namespace MiniBlogiv2
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[]? Picture { get; set; }
        public virtual ICollection<Note>? Comment
        {
            get; set;
        }
    }
}