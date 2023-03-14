using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSki.Controllers
{
    public class CookieControler : Controller
    {
        public IActionResult Index()
        {
            if (Request.Cookies["name"]!=null)
            {
                ViewBag.message = Request.Cookies["name"].ToString();
            }
            else
            {
                ViewBag.message = "brak".ToString();
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormCollection fc)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Append("name", fc["txtcookie"], options);
            return RedirectToAction("Index");
        }

    }
}
