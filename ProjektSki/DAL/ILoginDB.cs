using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektSki.Models;

namespace ProjektSki.DAL
{
    public interface ILoginDB
    {
        public List<SiteUser> LoadUsers();
        public SiteUser LoadUser(SiteUser p);
        public string Hasher(string password);
        public void Create(SiteUser p);

    }
}
