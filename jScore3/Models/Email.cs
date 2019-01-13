using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jScore3.Models
{
    public class Email
    {
        [Display(Name = "Odbiorca")]
        public string To { get; set; }
        public string From { get; set; }
        [Display(Name = "Temat wiadomości")]
        public string Subject { get; set; }
        [Display(Name = "Treść wiadomości")]
        public string Body { get; set; }

    }
}