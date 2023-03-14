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

namespace ProjektSki.Pages.Products
{
    [Authorize(Roles = "worker,manager,admin")]
    public class EditModel : PageModel
    {
        private readonly ProjektSki.Data.ShopContext _context;

        public EditModel(ProjektSki.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product
                .Include(p => p.Producer).FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            Categories = _context.Category.ToList();
            //Producers = _context.Producer.ToList();
            ViewData["ProducerId"] = new SelectList(_context.Set<Producer>(), "Id", "Name");
            return Page();
        }
        [BindProperty]
        public int categoryID { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Product.Categories = new List<Category>();
            Product.Categories.Add(_context.Category.FirstOrDefault(x => x.Id == categoryID));
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Product).State = EntityState.Modified;
            _context.Update(Product); //dorobic try catch jezeli jest ta sama kategoria 
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
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

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
