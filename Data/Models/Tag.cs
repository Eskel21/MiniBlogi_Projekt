using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBlogi_Projekt.Data.Models
{
    public class Tag
    {
        [Key] public int TagId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        public ICollection<TagNote>? TagNotes { get; set; }
    }
}
