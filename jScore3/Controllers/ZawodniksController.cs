using System;
using System.Collections.Generic;
using System.Data;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using jScore3.DAL;
using jScore3.Models;

namespace jScore3.Controllers
{
    public class ZawodniksController : Controller
    {
        private ScoreContext db = new ScoreContext();
        // GET: Zawodniks
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {


            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var zawodnicys = from s in db.Zawodnicy.Include(z => z.Klub) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                zawodnicys = zawodnicys.Where(s => s.Nazwisko.Contains(searchString) || s.Klub.Nazwa.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "lastname_desc":
                    zawodnicys = zawodnicys.OrderByDescending(s => s.Nazwisko);
                    break;
                default:
                    zawodnicys = zawodnicys.OrderBy(s => s.Nazwisko);
                    break;
            }




            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(zawodnicys.ToPagedList(pageNumber, pageSize));

            //return View(zawodnicys.ToList());
        }

        // GET: Zawodniks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zawodnik zawodnik = db.Zawodnicy.Find(id);
            if (zawodnik == null)
            {
                return HttpNotFound();
            }
            return View(zawodnik);
        }

        // GET: Zawodniks/Create
        public ActionResult Create()
        {
            ViewBag.KlubId = new SelectList(db.Kluby, "Id", "Nazwa");
            return View();
        }

        // POST: Zawodniks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Imie,Nazwisko,KrajPochodzenia,DataUrodzenia,Wzrost,Waga,NumerKoszulki,KlubId")] Zawodnik zawodnik)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["zdjecieZawodnik"];
                if (file != null && file.ContentLength > 0)
                {
                    zawodnik.Zdjecie = System.Guid.NewGuid().ToString();
                    zawodnik.Zdjecie = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Zawodnicy/") + zawodnik.Zdjecie);
                }

                db.Zawodnicy.Add(zawodnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlubId = new SelectList(db.Kluby, "Id", "Nazwa", zawodnik.KlubId);
            return View(zawodnik);
        }

        // GET: Zawodniks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zawodnik zawodnik = db.Zawodnicy.Find(id);
            if (zawodnik == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlubId = new SelectList(db.Kluby, "Id", "Nazwa", zawodnik.KlubId);
            return View(zawodnik);
        }

        // POST: Zawodniks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imie,Nazwisko,KrajPochodzenia,DataUrodzenia,Wzrost,Waga,NumerKoszulki,KlubId")] Zawodnik zawodnik)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["zdjecieZawodnik"];
                if (file != null && file.ContentLength > 0)
                {
                    zawodnik.Zdjecie = System.Guid.NewGuid().ToString();
                    zawodnik.Zdjecie = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Zawodnicy/") + zawodnik.Zdjecie);
                }
                db.Entry(zawodnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlubId = new SelectList(db.Kluby, "Id", "Nazwa", zawodnik.KlubId);
            return View(zawodnik);
        }

        // GET: Zawodniks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zawodnik zawodnik = db.Zawodnicy.Find(id);
            if (zawodnik == null)
            {
                return HttpNotFound();
            }
            return View(zawodnik);
        }

        // POST: Zawodniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zawodnik zawodnik = db.Zawodnicy.Find(id);
            db.Zawodnicy.Remove(zawodnik);
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
