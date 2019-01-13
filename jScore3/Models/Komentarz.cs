using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jScore3.Models
{
    public class Komentarz
    {
        public int Id { get; set; }
        public string Tresc { get; set; }
        public int? MeczId { get; set; }
        public int? ProfilId { get; set; }

        public virtual Profil Profil { get; set; }
        public virtual Mecz Mecz { get; set; }

    }
}