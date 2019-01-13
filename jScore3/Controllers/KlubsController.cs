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

namespace jScore3.Controllers
{
    public class KlubsController : Controller
    {
        private ScoreContext db = new ScoreContext();

        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var kluby = from s in db.Kluby
                        select s;
            switch (sortOrder)
            {
                case "name_desc":
                    kluby = kluby.OrderByDescending(s => s.Nazwa);
                    break;
                case "Date":
                    kluby = kluby.OrderBy(s => s.DataPowstania);
                    break;
                case "date_desc":
                    kluby = kluby.OrderByDescending(s => s.DataPowstania);
                    break;
                default:
                    kluby = kluby.OrderBy(s => s.Nazwa);
                    break;
            }
            return View(kluby.ToList());
        }

        // GET: Klubs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klub klub = db.Kluby.Find(id);
            if (klub == null)
            {
                return HttpNotFound();
            }
            return View(klub);
        }

        public ActionResult Detailss()
        {
            dynamic zawodnicyy =
                (from k in db.Kluby
                    join z in db.Zawodnicy on k.Id equals z.KlubId
                    select new
                    {
                        z.Id,
                        z.Imie,
                        z.Nazwisko,
                        z.KlubId,
                        z.Klub,
                    }).AsEnumerable().Select(jj => jj.ToExpando());


            return PartialView(zawodnicyy);
        }

        // GET: Klubs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Klubs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa,Adres,KodPocztowy,Wojewodztwo,Barwy,DataPowstania,Herb")] Klub klub)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["herbKlub"];
                if (file != null && file.ContentLength > 0)
                {
                    klub.Herb = System.Guid.NewGuid().ToString();
                    klub.Herb = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Herby") + klub.Herb);
                }
                db.Kluby.Add(klub);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klub);
        }

        // GET: Klubs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klub klub = db.Kluby.Find(id);
            if (klub == null)
            {
                return HttpNotFound();
            }
            return View(klub);
        }

        // POST: Klubs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa,Adres,KodPocztowy,Wojewodztwo,Barwy,DataPowstania,Herb")] Klub klub)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["herbKlub"];
                if (file != null && file.ContentLength > 0)
                {
                    klub.Herb = System.Guid.NewGuid().ToString();
                    klub.Herb = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Herby") + klub.Herb);
                }
                db.Entry(klub).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klub);
        }

        // GET: Klubs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klub klub = db.Kluby.Find(id);
            if (klub == null)
            {
                return HttpNotFound();
            }
            return View(klub);
        }

        // POST: Klubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klub klub = db.Kluby.Find(id);
            db.Kluby.Remove(klub);
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
