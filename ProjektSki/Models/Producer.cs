using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjektSki.Models
{
    public class Producer
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa producenta")]
        [Required(ErrorMessage = "{0} jest wymagana")]
        [MaxLength(50, ErrorMessage = "Maksymalna długość {0} to {1}")]
        public string Name { get; set; }
        [Display(Name = "Kod kraju")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Dozwolone jedynie litery")]
        [Required(ErrorMessage = "{0} jest wymagany")]
        public string Country { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
