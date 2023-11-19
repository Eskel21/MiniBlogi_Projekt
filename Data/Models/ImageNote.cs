using System.Text.RegularExpressions;
using System;

namespace MiniBlogi_Projekt.Data.Models
{
    public class ImageNote
    {
        public int NoteId { get; set; } 
        public Note Note { get; set; }
        public int ImageId { get; set; } 
        public Image Image { get; set; }

    }
}
