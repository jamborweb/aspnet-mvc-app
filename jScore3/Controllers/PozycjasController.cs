using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using jScore3.DAL;
using jScore3.ExMethod;
using jScore3.Models;
using WebGrease.Css.Ast.Selectors;

namespace jScore3.Controllers
{
    public class PozycjasController : Controller
    {
        private ScoreContext db = new ScoreContext();

        // GET: Pozycjas
        public ActionResult Index()
        {

            var pozycje = db.Pozycje.Include(p => p.Klub).Include(p => p.Mecz).Include(p => p.Zawodnik);
            return View(pozycje.ToList());
        }

        public ActionResult Indexx()
        {
            dynamic pozycjee =
                (from m in db.Mecze
                join p in db.Pozycje on m.Id equals p.MeczId
                join z in db.Zawodnicy on p.ZawodnikId equals z.Id
                select new
                {
                    MeczId = m.Id,
                    m.GoscKlub,
                    m.GoscKlubId,
                    m.GospodarzKlub,
                    m.GospodarzKlubId,
                    PozycjaId = p.Id,
                    NazwaPozycji = p.Nazwa,
                    ZawodnikId = z.Id,
                    z.Imie,
                    z.Nazwisko,
                    z.Klub
                }).AsEnumerable().Select(jj => jj.ToExpando());


            return PartialView(pozycjee);
        }



        // GET: Pozycjas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pozycja pozycja = db.Pozycje.Find(id);
            if (pozycja == null)
            {
                return HttpNotFound();
            }
            return View(pozycja);
        }

        // GET: Pozycjas/Create
        public ActionResult Create()
        {
            ViewBag.KlubId = new SelectList(db.Kluby, "Id", "Nazwa");
            ViewBag.MeczId = new SelectList(db.Mecze, "Id", "Id");
            ViewBag.ZawodnikId = new SelectList(db.Zawodnicy, "Id", "Imie");
            return View();
        }

        // POST: Pozycjas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa,MeczId,ZawodnikId,KlubId")] Pozycja pozycja)
        {
            if (ModelState.IsValid)
            {
                db.Pozycje.Add(pozycja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlubId = new SelectList(db.Kluby, "Id", "Nazwa", pozycja.KlubId);
            ViewBag.MeczId = new SelectList(db.Mecze, "Id", "Id", pozycja.MeczId);
            ViewBag.ZawodnikId = new SelectList(db.Zawodnicy, "Id", "Imie", pozycja.ZawodnikId);
            return View(pozycja);
        }

        // GET: Pozycjas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pozycja pozycja = db.Pozycje.Find(id);
            if (pozycja == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlubId = new SelectList(db.Kluby, "Id", "Nazwa", pozycja.KlubId);
            ViewBag.MeczId = new SelectList(db.Mecze, "Id", "Id", pozycja.MeczId);
            ViewBag.ZawodnikId = new SelectList(db.Zawodnicy, "Id", "Imie", pozycja.ZawodnikId);
            return View(pozycja);
        }

        // POST: Pozycjas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa,MeczId,ZawodnikId,KlubId")] Pozycja pozycja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pozycja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlubId = new SelectList(db.Kluby, "Id", "Nazwa", pozycja.KlubId);
            ViewBag.MeczId = new SelectList(db.Mecze, "Id", "Id", pozycja.MeczId);
            ViewBag.ZawodnikId = new SelectList(db.Zawodnicy, "Id", "Imie", pozycja.ZawodnikId);
            return View(pozycja);
        }

        // GET: Pozycjas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pozycja pozycja = db.Pozycje.Find(id);
            if (pozycja == null)
            {
                return HttpNotFound();
            }
            return View(pozycja);
        }

        // POST: Pozycjas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pozycja pozycja = db.Pozycje.Find(id);
            db.Pozycje.Remove(pozycja);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
