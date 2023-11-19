using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBlogi_Projekt.Data.Models
{
    public class Comment
    {

        [Key] public int Id_comment { get; set; }
        [Required]
        public DateTime Date_comment { get; set; }

        [Required]
        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string Content { get; set; }

        [Required]
        public string Ip_computer { get; set; }
        [Required]
        public int NoteId { get; set; }
        public virtual Note Note { get; set; }
    }
}
