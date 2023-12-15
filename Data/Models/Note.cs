using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniBlogiv2.Data.Models
{
    public class Note
    {
        [Key] public int NoteId { get; set; }
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }

        [MaxLength(256)]
        [Column(TypeName = "varchar(256)")]
        public string Description { get; set; }

        [Required]
        [MaxLength(500)]
        [Column(TypeName = "varchar(MAX)")]
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Comment>? Comment
        {
            get; set;
        }
        public ICollection<TagNote>? TagNotes { get; set; }
        public ICollection<ImageNote>? ImageNotes { get; set; }
    }
}
