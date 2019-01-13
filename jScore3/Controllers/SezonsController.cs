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
    public class SezonsController : Controller
    {
        private ScoreContext db = new ScoreContext();

        public ActionResult Index()
        {
            return View(db.Sezony.ToList());
        }

        // GET: Sezons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sezon sezon = db.Sezony.Find(id);
            if (sezon == null)
            {
                return HttpNotFound();
            }
            return View(sezon);
        }

        // GET: Sezons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sezons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa")] Sezon sezon)
        {
            if (ModelState.IsValid)
            {
                db.Sezony.Add(sezon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sezon);
        }

        // GET: Sezons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sezon sezon = db.Sezony.Find(id);
            if (sezon == null)
            {
                return HttpNotFound();
            }
            return View(sezon);
        }

        // POST: Sezons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa")] Sezon sezon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sezon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sezon);
        }

        // GET: Sezons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sezon sezon = db.Sezony.Find(id);
            if (sezon == null)
            {
                return HttpNotFound();
            }
            return View(sezon);
        }

        // POST: Sezons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sezon sezon = db.Sezony.Find(id);
            db.Sezony.Remove(sezon);
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
