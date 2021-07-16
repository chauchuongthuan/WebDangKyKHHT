using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDangKyKHHT.Models;

namespace WebDangKyKHHT.Controllers
{
    [Authorize]
    public class DeadlinesController : Controller
    {
        private SEP_TEAM15_WEBKHHTEntities db = new SEP_TEAM15_WEBKHHTEntities();

        // GET: Deadlines
        public ActionResult Index()
        {
            return View(db.Deadlines.ToList());
        }

        // GET: Deadlines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deadline deadline = db.Deadlines.Find(id);
            if (deadline == null)
            {
                return HttpNotFound();
            }
            return View(deadline);
        }

        // GET: Deadlines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deadlines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateStart,DateEnd,DangKy")] Deadline deadline)
        {
            if (ModelState.IsValid)
            {
                db.Deadlines.Add(deadline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deadline);
        }

        // GET: Deadlines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deadline deadline = db.Deadlines.Find(id);
            if (deadline == null)
            {
                return HttpNotFound();
            }
            return View(deadline);
        }

        // POST: Deadlines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateStart,DateEnd,DangKy")] Deadline deadline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deadline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deadline);
        }

        // GET: Deadlines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deadline deadline = db.Deadlines.Find(id);
            if (deadline == null)
            {
                return HttpNotFound();
            }
            return View(deadline);
        }

        // POST: Deadlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deadline deadline = db.Deadlines.Find(id);
            db.Deadlines.Remove(deadline);
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
