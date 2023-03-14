using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjektSki.Controllers;

namespace ProjektSki.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public string message { get; set; }

        public string pathh { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            if(HttpContext.Request.Cookies["name"] !=null)
            {
                pathh = HttpContext.Request.Cookies["name"].ToString();
            }
            else
            {
                pathh = "~/Images/waterskier.jpg";
            }
        }
        public void OnPost()
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Append("name", message, options);
            pathh = message;
        }
    }
}
