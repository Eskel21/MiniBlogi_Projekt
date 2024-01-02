using Microsoft.AspNetCore.Identity;
using MiniBlogiv2.Data.Models;
using System.Net;

namespace MiniBlogiv2
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[]? Picture { get; set; }
        public virtual ICollection<Note>? Notes
        {
            get; set;
        }
        
    }
}