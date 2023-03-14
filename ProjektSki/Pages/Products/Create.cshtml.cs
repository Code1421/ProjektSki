using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjektSki.Data;
using ProjektSki.Models;

namespace ProjektSki.Pages.Products
{
    [Authorize(Roles = "worker,manager,admin")]
    public class CreateModel : PageModel
    {
        private readonly ProjektSki.Data.ShopContext _context;
        public List<Category> Categories { get; set; }

        public CreateModel(ProjektSki.Data.ShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Categories = _context.Category.ToList();
            //Producers = _context.Producer.ToList();
            ViewData["ProducerId"] = new SelectList(_context.Set<Producer>(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public int categoryID { get; set; }

        public CategoryProduct Category { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Product.Categories = new List<Category>();
            Product.Categories.Add( _context.Category.FirstOrDefault(x => x.Id == categoryID));
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
