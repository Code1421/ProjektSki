using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektSki.DAL;

namespace ProjektSki.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class AccessLogsModel : PageModel
    {
        public List<string> logs;
        IXMLService service;

        public AccessLogsModel(IXMLService _service)
        {
            service = _service;
        }
        public void OnGet()
        {
            logs = service.Get();
        }
    }
}
