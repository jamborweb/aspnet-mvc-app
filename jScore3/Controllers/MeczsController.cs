using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using jScore3.DAL;
using jScore3.Models;

namespace jScore3.Controllers
{
    public class MeczsController : Controller
    {
        private ScoreContext db = new ScoreContext();

        // GET: Meczs
        public ActionResult Index()
        {
            var mecze = db.Mecze.Include(m => m.GoscKlub).Include(m => m.GospodarzKlub).Include(m => m.Sezon)
                .Include(m => m.Stadion);
            return View(mecze.ToList());
        }

        // GET: Meczs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mecz mecz = db.Mecze.Find(id);
            if (mecz == null)
            {
                return HttpNotFound();
            }

            return View(mecz);
        }

        // GET: Meczs/Create
        public ActionResult Create()
        {
            ViewBag.GoscKlubId = new SelectList(db.Kluby, "Id", "Nazwa");
            ViewBag.GospodarzKlubId = new SelectList(db.Kluby, "Id", "Nazwa");
            ViewBag.SezonId = new SelectList(db.Sezony, "Id", "Nazwa");
            ViewBag.StadionId = new SelectList(db.Stadiony, "Id", "Nazwa");
            return View();
        }

        // POST: Meczs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
                "Id,Kolejka,GolGospodarz,GolGosc,LiczbaWidzow,Data,GospodarzKlubId,GoscKlubId,SezonId,StadionId")]
            Mecz mecz)
        {
            if (ModelState.IsValid)
            {
                db.Mecze.Add(mecz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GoscKlubId = new SelectList(db.Kluby, "Id", "Nazwa", mecz.GoscKlubId);
            ViewBag.GospodarzKlubId = new SelectList(db.Kluby, "Id", "Nazwa", mecz.GospodarzKlubId);
            ViewBag.SezonId = new SelectList(db.Sezony, "Id", "Nazwa", mecz.SezonId);
            ViewBag.StadionId = new SelectList(db.Stadiony, "Id", "Nazwa", mecz.StadionId);
            return View(mecz);
        }

        // GET: Meczs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mecz mecz = db.Mecze.Find(id);
            if (mecz == null)
            {
                return HttpNotFound();
            }

            ViewBag.GoscKlubId = new SelectList(db.Kluby, "Id", "Nazwa", mecz.GoscKlubId);
            ViewBag.GospodarzKlubId = new SelectList(db.Kluby, "Id", "Nazwa", mecz.GospodarzKlubId);
            ViewBag.SezonId = new SelectList(db.Sezony, "Id", "Nazwa", mecz.SezonId);
            ViewBag.StadionId = new SelectList(db.Stadiony, "Id", "Nazwa", mecz.StadionId);
            return View(mecz);
        }

        // POST: Meczs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
                "Id,Kolejka,GolGospodarz,GolGosc,LiczbaWidzow,Data,GospodarzKlubId,GoscKlubId,SezonId,StadionId")]
            Mecz mecz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mecz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GoscKlubId = new SelectList(db.Kluby, "Id", "Nazwa", mecz.GoscKlubId);
            ViewBag.GospodarzKlubId = new SelectList(db.Kluby, "Id", "Nazwa", mecz.GospodarzKlubId);
            ViewBag.SezonId = new SelectList(db.Sezony, "Id", "Nazwa", mecz.SezonId);
            ViewBag.StadionId = new SelectList(db.Stadiony, "Id", "Nazwa", mecz.StadionId);
            return View(mecz);
        }

        // GET: Meczs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mecz mecz = db.Mecze.Find(id);
            if (mecz == null)
            {
                return HttpNotFound();
            }

            return View(mecz);
        }

        // POST: Meczs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mecz mecz = db.Mecze.Find(id);
            db.Mecze.Remove(mecz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult AddComment(int? meczId)
        {
            if (!meczId.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var mecze = db.Mecze.FirstOrDefault(n => n.Id == meczId.Value);

            if (mecze == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            ViewBag.MeczId = meczId;

            return PartialView("_AddComment");
        }

        [HttpPost]
        public ActionResult AddComment(string comment)
        {

            Komentarz _komentarzMaster = new Komentarz();

                _komentarzMaster.Tresc = comment;
                // _meczEncja.Komentarze.Add(_komentarzMaster);
                db.Komentarze.Add(_komentarzMaster);
                db.SaveChanges();
               // return 1;

            return Json(new {Success = true});

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
