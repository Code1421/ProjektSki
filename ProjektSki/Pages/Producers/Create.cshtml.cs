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
using ProjektSki.DAL;

namespace ProjektSki.Pages.Producers
{
    [Authorize(Roles = "manager,admin")]
    public class CreateModel : PageModel
    {
        private readonly ProjektSki.Data.ShopContext _context;

        public CreateModel(ProjektSki.Data.ShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Producer Producer { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        /*
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            _context.Producer_1.Add(Producer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        */
        public IActionResult OnPost(Producer Producer)
        {
            if (ModelState.IsValid)
            {
                ProductDB.ProducerCreate(Producer);
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
