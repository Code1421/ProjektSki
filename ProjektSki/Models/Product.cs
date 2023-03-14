using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjektSki.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa produktu")]
        [Required(ErrorMessage = "{0} jest wymagana")]
        [MaxLength(50, ErrorMessage = "Maksymalna długość {0} to {1}")]
        public string Name { get; set; }
        [Display(Name = "Cena")]
        [Required(ErrorMessage = "{0} jest wymagana")]
        public double Price { get; set; }

        [Display(Name = "Producent")]
        public int ProducerId { get; set; }
        [Display(Name = "Producent")]
        public Producer Producer { get; set; }

        public List<Category> Categories { get; set; }

    }
}
