using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jScore3.Models
{
    public class Klub
    {
        public int Id { get; set; }

        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "Nazwa klubu")]
        [Required]
        public string Nazwa { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Adres { get; set; }

        [Display(Name = "Kod Pocztowy")]
        [RegularExpression(@"[0-9]{2}(\-[0-9]{3})?", ErrorMessage = "Nieprawidłowy format kodu pocztowego. Przykład: XX-XXX")]
        public string KodPocztowy { get; set; }

        [Display(Name = "Województwo")]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Wojewodztwo { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Barwy klubu")]
        [Required]
        public string Barwy { get; set; }

        [Display(Name = "Data Powstania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataPowstania { get; set; }

        public string Herb { get; set; }

        public virtual List<Zawodnik> Zawodnicy { get; set; }
        public virtual List<Mecz> MeczeGospodarzy { get; set; }
        public virtual List<Mecz> MeczeGosci { get; set; }
        public virtual List<Pozycja> PozycjeGospodarzy { get; set; }
        public virtual List<Pozycja> PozycjeGoscia { get; set; }
    }
}