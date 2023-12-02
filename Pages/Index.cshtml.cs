using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System;

using MiniBlogiv2.Data.Models;
using MiniBlogiv2.Data;

namespace MiniBlogiv2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IList<Comment> Comments { get; set; }
        public void OnGet()
        {
            Comments = _context.Comment.ToList();
        }


    }
}