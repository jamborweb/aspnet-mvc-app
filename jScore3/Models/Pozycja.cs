using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jScore3.Models
{
    public class Pozycja
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa pozycji")]
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Nazwa { get; set; }

        public int MeczId { get; set; }
        public int ZawodnikId { get; set; }
        public int KlubId { get; set; }

        public virtual Mecz Mecz { get; set; }
        public virtual Zawodnik Zawodnik { get; set; }
        public virtual Klub Klub { get; set; }
    }
}