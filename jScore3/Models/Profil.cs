using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jScore3.Models
{
    public class Profil
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public virtual List<Komentarz> Komentarze { get; set; }
        public virtual List<Klub> FavouritesKlubs { get; set; }
    }
}