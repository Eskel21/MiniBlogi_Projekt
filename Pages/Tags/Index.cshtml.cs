﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniBlogiv2.Data;
using MiniBlogiv2.Data.Models;

namespace MiniBlogiv2.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private readonly MiniBlogiv2.Data.ApplicationDbContext _context;

        public IndexModel(MiniBlogiv2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Tag> Tag { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Tag != null)
            {
                Tag = await _context.Tag.ToListAsync();
            }
        }
    }
}
