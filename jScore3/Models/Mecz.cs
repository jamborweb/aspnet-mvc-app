using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jScore3.Models
{
    public class Mecz
    {
        public int Id { get; set; }

        [Display(Name = "Numer kolejki")]
        [Range(1, 40)]
        [Required]
        public int Kolejka { get; set; }

        [Display(Name = "Gole gospodarzy")]
        [Range(0, 40)]
        [Required]
        public int GolGospodarz { get; set; }

        [Display(Name = "Gole gości")]
        [Range(0, 40)]
        [Required]
        public int GolGosc { get; set; }

        [Display(Name = "Liczba widzów")]
        [Range(0, 60000)]
        [Required]
        public int LiczbaWidzow { get; set; }

        [Display(Name = "Data spotkania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Data { get; set; }

        public int GospodarzKlubId { get; set; }

        public int GoscKlubId { get; set; }

        public int SezonId { get; set; }

        public int StadionId { get; set; }

        public virtual Sezon Sezon { get; set; }
        public virtual Stadion Stadion { get; set; }
        public virtual Klub GospodarzKlub { get; set; }
        public virtual Klub GoscKlub { get; set; }
        public virtual List<Pozycja> Pozycje { get; set; }
        public virtual List<Komentarz> Komentarze { get; set; }


    }
}