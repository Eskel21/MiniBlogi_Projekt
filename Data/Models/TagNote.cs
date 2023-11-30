using System.Text.RegularExpressions;
using System;

namespace MiniBlogi_Projekt.Data.Models
{
    public class TagNote
    {
        public int NoteId { get; set; } 
        public Note Note { get; set; }
        public int TagId { get; set; } 
        public Tag Tag { get; set; }
        
    }
}
