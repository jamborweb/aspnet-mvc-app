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
    public class StadionsController : Controller
    {
        private ScoreContext db = new ScoreContext();
        // GET: Stadions
        public ActionResult Index()
        {
            return View(db.Stadiony.ToList());
        }

        // GET: Stadions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadion stadion = db.Stadiony.Find(id);
            if (stadion == null)
            {
                return HttpNotFound();
            }
            return View(stadion);
        }

        // GET: Stadions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stadions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa,Pojemnosc,Miasto,Zdjecie")] Stadion stadion)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["zdjecieStadion"];
                if (file != null && file.ContentLength > 0)
                {
                    stadion.Zdjecie = System.Guid.NewGuid().ToString();
                    stadion.Zdjecie = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + stadion.Zdjecie);
                }
                db.Stadiony.Add(stadion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stadion);
        }

        // GET: Stadions/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadion stadion = db.Stadiony.Find(id);
            if (stadion == null)
            {
                return HttpNotFound();
            }
            return View(stadion);
        }

        // POST: Stadions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Nazwa,Pojemnosc,Miasto,Zdjecie")] Stadion stadion)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["zdjecieStadion"];
                if (file != null && file.ContentLength > 0)
                {
                    stadion.Zdjecie = System.Guid.NewGuid().ToString();
                    stadion.Zdjecie = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + stadion.Zdjecie);
                }
                db.Entry(stadion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stadion);
        }

        // GET: Stadions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadion stadion = db.Stadiony.Find(id);
            if (stadion == null)
            {
                return HttpNotFound();
            }
            return View(stadion);
        }

        // POST: Stadions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stadion stadion = db.Stadiony.Find(id);
            db.Stadiony.Remove(stadion);
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
