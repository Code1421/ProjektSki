using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjektSki.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole obowiazkowe")]
        [MaxLength(20, ErrorMessage = "Maksymalna długość {0} to {1}")]
        [RegularExpression(@"^[a-zA-Z\s]*$",ErrorMessage ="Dozwolone jedynie litery")]
        [Display(Name = "Krótka nazwa")]
        public string ShortName { get; set; }
        [Required(ErrorMessage = "Pole obowiazkowe")]
        [MaxLength(50, ErrorMessage = "Maksymalna długość {0} to {1}")]
        [Display(Name = "Długa nazwa")]
        public string LongName { get; set; }
        public List<Product> Products { get; set; }
    }
}
