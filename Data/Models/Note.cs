using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace MiniBlogi_Projekt.Data.Models
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


        public int Id_user { get; set; }
        [Required]
        [MaxLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string Content { get; set; }
        public virtual ICollection<Comment>? Comment
        {
            get; set;
        }
        public ICollection<TagNote>? TagNotes { get; set; }
        public ICollection<ImageNote>? ImageNotes { get; set; }
    }
}