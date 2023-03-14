using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjektSki.Models
{
    public class SiteUser
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(ErrorMessage = "{0} jest wymagana")]
        public string userName { get; set; }
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "{0} jest wymagane")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "Rola")]
        public string role { get; set; }

    }
}
