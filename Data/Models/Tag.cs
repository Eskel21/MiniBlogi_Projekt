using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniBlogiv2.Data.Models
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
