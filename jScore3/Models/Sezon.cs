using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jScore3.Models
{
    public class Sezon
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa sezonu")]
        [StringLength(9, MinimumLength = 5)]
        [Required]
        public string Nazwa { get; set; }
        public virtual List<Mecz> Mecze { get; set; }
    }
}