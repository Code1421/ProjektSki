using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektSki.Data;
using ProjektSki.Models;
using ProjektSki.DAL;

namespace ProjektSki.Pages.Producers
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        private readonly ProjektSki.Data.ShopContext _context;

        public DeleteModel(ProjektSki.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        /*
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producer = await _context.Producer_1.FindAsync(id);

            if (Producer != null)
            {
                _context.Producer_1.Remove(Producer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        */
        public IActionResult OnPost(Producer p)
        {
            ProductDB.ProducerDelete(p.Id);
            return RedirectToPage("Index");
        }
    }
}
