using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ProjektSki.Models;
using ProjektSki.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ProjektSki.Pages.Login
{
    public class UserLoginModel : PageModel
    {
        //private readonly IConfiguration _configuration; //---bez DI
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public List<SiteUser> users;
        
        //do wstrzykiwania 
        ILoginDB serwisloginDB;
        public UserLoginModel(ILoginDB _loginDB)
        {
            serwisloginDB = _loginDB;
        }

        /*public UserLoginModel(IConfiguration configuration) //---bez DI
        {
            _configuration = configuration;
        }
        */
        public void OnGet()
        {
            users = serwisloginDB.LoadUsers();
            //users = LoginDB.LoadUsers(); //---bez DI
        }

        private bool ValidateUser(SiteUser user)
        {
            users = serwisloginDB.LoadUsers();
            //users = LoginDB.LoadUsers(); //---bez DI
            int dlugosc = users.Count();
            //usuwanie bialych znakow
            foreach (SiteUser user1 in users)
            {
                //user1 to sa dane z bazy

                //var passwordHasher2 = new PasswordHasher<string>();
                //passwordHasher2.GetHashCode();

                //to bez hashowania hasla
                //if ((user.userName == user1.userName) && (user.password == user1.password))
                if (user.userName == user1.userName)
                {
                    var passwordHasher = serwisloginDB.Hasher(user.password);
                    //var passwordHasher = LoginDB.Hasher(user.password);

                    //var passwordHasher = new PasswordHasher<string>();
                    //if (passwordHasher.VerifyHashedPassword(null, user1.password, user.password) == PasswordVerificationResult.Success)
                    if (passwordHasher == user1.password)
                    {
                        return true;
                        break;
                    }
                }
            }
            return false;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ValidateUser(user))
            {
                returnUrl = "/Index";
                user = serwisloginDB.LoadUser(user);
                HttpContext.Session.Set(user.userName, BitConverter.GetBytes(true));
                //user=LoginDB.LoadUser(user); //---bez DI
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.userName),
                    new Claim(ClaimTypes.Role,user.role)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));

                return Redirect(returnUrl);
            }
            return Page();
        }
    }
}
