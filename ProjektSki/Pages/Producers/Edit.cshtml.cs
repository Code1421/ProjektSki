using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektSki.Data;
using ProjektSki.Models;
using ProjektSki.DAL;

namespace ProjektSki.Pages.Producers
{
    [Authorize(Roles = "manager,admin")]
    public class EditModel : PageModel
    {
        private readonly ProjektSki.Data.ShopContext _context;

        public EditModel(ProjektSki.Data.ShopContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Producer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProducerExists(Producer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
        

        private bool ProducerExists(int id)
        {
            return _context.Producer_1.Any(e => e.Id == id);
        }
        /*
        public IActionResult OnPost(Producer newDetailsProducer)
        {
            if (ModelState.IsValid)
            {
                ProductDB.ProducerEdit(newDetailsProducer);
                return RedirectToPage("List");
            }
            else
            {
                Producer = newDetailsProducer;
                return Page();
            }
        }
        */
    }
}
