using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniBlogiv2.Data.Models
{
    public class Image
    {
        [Key] public int ImageId { get; set; }
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }

        public byte[] ImageData { get; set; }
        public ICollection<ImageNote>? ImageNotes { get; set; }
    }
}
