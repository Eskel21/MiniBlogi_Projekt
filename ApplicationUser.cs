using Microsoft.AspNetCore.Identity;
using MiniBlogi_Projekt.Data.Models;

namespace MiniBlogi_Projekt
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