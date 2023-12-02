using System.Text.RegularExpressions;
using System;
namespace MiniBlogiv2.Data.Models
{
    public class ImageNote
    {
        public int NoteId { get; set; }
        public Note Note { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }

    }
}
