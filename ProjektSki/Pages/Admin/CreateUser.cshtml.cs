using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektSki.DAL;
using ProjektSki.Models;

namespace ProjektSki.Pages.Admin
{   
    //autoryzacja danego uzytkownika
    [Authorize(Roles = "admin")]
    public class CreateUserModel : PageModel
    {
        public SiteUser user { get; set; }
        
        public SiteUser password_val { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost(SiteUser user,SiteUser password_val)
        {
            
            if(user.password == password_val.password && !string.IsNullOrEmpty(user.userName) && !string.IsNullOrEmpty(user.password) && !string.IsNullOrEmpty(password_val.password))
            {
                LoginDB.Create(user);
                return RedirectToPage("../Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
