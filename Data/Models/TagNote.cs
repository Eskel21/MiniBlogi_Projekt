using System.Text.RegularExpressions;
using System;

namespace MiniBlogi_Projekt.Data.Models
{
    public class TagNote
    {
        public int NoteId { get; set; } //klucz obcy do Person
        public Note Note { get; set; }
        public int TagId { get; set; } //klucz obcy do Group
        public Tag Tag { get; set; }
        
    }
}
