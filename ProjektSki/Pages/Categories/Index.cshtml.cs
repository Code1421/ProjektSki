using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektSki.DAL;
using ProjektSki.Models;

namespace ProjektSki.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ProjektSki.Data.ShopContext _context;

        public IndexModel(ProjektSki.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }
        public IList<Category> sp_Category { get; set; }

        public async Task OnGetAsync()
        {
            //Category = await _context.Category.ToListAsync();
            sp_Category = await _context.Category.Include(x => x.Products).ToListAsync();
            Category = ProductDB.LoadCategory();
        }
    }
}
