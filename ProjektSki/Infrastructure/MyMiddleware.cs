using Microsoft.AspNetCore.Http;
using ProjektSki.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace ProjektSki.Infrastructure
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IXMLService _service;

        public MyMiddleware(RequestDelegate next, IXMLService service)
        {
            _next = next;
            _service = service;

        }

        public Task Invoke(HttpContext httpContext)
        {
            string access;
            string accessed;
            access = httpContext.Request.Path.Value;
            accessed = httpContext.Request.Path.Value;
            if(access.Contains("AccessDenied"))
            {
                var user = httpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("name")).Value.ToString();
                var role = httpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("role")).Value.ToString();
                var ac = httpContext.Session.GetString("accesspath").ToString();

                StringBuilder builder = new StringBuilder();
                builder.Append("Uzytkownik ");
                builder.Append(user);
                builder.Append(" o roli ");
                builder.Append(role);
                builder.Append(" probowal sie dostac o godzinie ");
                builder.Append(DateTime.Now.ToString());
                builder.Append(" do strony ");
                builder.Append(ac);
                builder.Append(" do ktorej nie ma dostepu.");
                

                _service.Add(builder.ToString());
            }
            else
            {
                httpContext.Session.SetString("accesspath",accessed );
            }
            return _next(httpContext);
        }
    }
}
