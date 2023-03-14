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
    public class IndexModel : PageModel
    {
        private readonly ProjektSki.Data.ShopContext _context;

        public IndexModel(ProjektSki.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Producer> Producer { get;set; }

        public async Task OnGetAsync()
        {
            //Producer = await _context.Producer_1.ToListAsync();
            Producer = await _context.Producer_1.Include(x => x.Products).ToListAsync();
        }
    }
}
