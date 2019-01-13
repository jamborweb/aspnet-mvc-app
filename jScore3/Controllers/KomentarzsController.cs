using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using jScore3.DAL;
using jScore3.ExMethod;
using jScore3.Models;
using Microsoft.AspNet.Identity;

namespace jScore3.Controllers
{
    public class KomentarzsController : Controller
    {
        private ScoreContext db = new ScoreContext();

        // GET: Komentarzs
        public ActionResult _Komentarze()
        {
            dynamic comment =
                (from m in db.Mecze
                    from k in db.Komentarze
                    where m.Id == k.MeczId
                    select new
                    {
                        k.Tresc
                    }).AsEnumerable().Select(x => x.ToExpando());

            return PartialView(comment);
        }

    }
}