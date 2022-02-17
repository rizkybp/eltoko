using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElToko.Models;

namespace ElToko.Controllers
{
    public class NasabahsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Nasabahs
        public ActionResult Index()
        {
            var data = db.Nasabahs.ToList();
            return View(db.Nasabahs.ToList());
        }

        // GET: Nasabahs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nasabah nasabah = db.Nasabahs.Find(id);
            if (nasabah == null)
            {
                return HttpNotFound();
            }
            return View(nasabah);
        }

        // GET: Nasabahs/Create
        public ActionResult Create()
        {
            Nasabah nasabah = new Nasabah();
            return View(nasabah);
        }

        // POST: Nasabahs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountId,Name")] Nasabah nasabah)
        {
            if (ModelState.IsValid)
            {
                //nasabah.AccountId = 0;
                db.Nasabahs.Add(nasabah);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nasabah);
        }

        // GET: Nasabahs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nasabah nasabah = db.Nasabahs.Find(id);
            if (nasabah == null)
            {
                return HttpNotFound();
            }
            return View(nasabah);
        }

        // POST: Nasabahs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountId,Name")] Nasabah nasabah)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nasabah).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nasabah);
        }

        // GET: Nasabahs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nasabah nasabah = db.Nasabahs.Find(id);
            if (nasabah == null)
            {
                return HttpNotFound();
            }
            return View(nasabah);
        }

        // POST: Nasabahs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nasabah nasabah = db.Nasabahs.Find(id);
            db.Nasabahs.Remove(nasabah);
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
