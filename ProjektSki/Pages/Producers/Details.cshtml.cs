using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektSki.Data;
using ProjektSki.Models;

namespace ProjektSki.Pages.Producers
{
    public class DetailsModel : PageModel
    {
        private readonly ProjektSki.Data.ShopContext _context;

        public DetailsModel(ProjektSki.Data.ShopContext context)
        {
            _context = context;
        }

        public Producer Producer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producer = await _context.Producer_1.FirstOrDefaultAsync(m => m.Id == id);

            if (Producer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
