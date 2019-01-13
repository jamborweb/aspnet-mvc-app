using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jScore3.Models
{
    public class Stadion
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa stadionu")]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Nazwa { get; set; }

        [Required]
        [Range(0, 100000)]
        public int Pojemnosc { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Miasto { get; set; }

        public string Zdjecie { get; set; }


        public virtual List<Mecz> Mecze { get; set; }
    }
}