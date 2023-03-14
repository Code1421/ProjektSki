using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektSki.Data;
using ProjektSki.Models;

namespace ProjektSki.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProjektSki.Data.ShopContext _context;

        public IndexModel(ProjektSki.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {

            Product = await _context.Product.Include(p => p.Categories).ToListAsync();
            Product = await _context.Product.Include(p => p.Producer).ToListAsync();
        }
    }
}
