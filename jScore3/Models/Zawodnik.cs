using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jScore3.Models
{
    public class Zawodnik
    {
        public int Id { get; set; }


        public string Imie { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Nazwisko { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        [Display(Name = "Kraj Pochodzenia")]
        public string KrajPochodzenia { get; set; }

        [Display(Name = "Data Urodzenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataUrodzenia { get; set; }

        public double Wzrost { get; set; }
        public double Waga { get; set; }
        [Display(Name = "Numer Koszulki")]
        [Range(0, 99)]
        public int NumerKoszulki { get; set; }
        public int KlubId { get; set; }

        public string Zdjecie { get; set; }


        public virtual Klub Klub { get; set; }
        public virtual List<Pozycja> Pozycje { get; set; }

    }
}