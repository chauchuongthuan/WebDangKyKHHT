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
    public class HocKisController : Controller
    {
        private SEP_TEAM15_WEBKHHTEntities db = new SEP_TEAM15_WEBKHHTEntities();

        // GET: HocKis
        public ActionResult Index()
        {
            return View(db.HocKis.ToList());
        }

        // GET: HocKis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocKi hocKi = db.HocKis.Find(id);
            if (hocKi == null)
            {
                return HttpNotFound();
            }
            return View(hocKi);
        }

        // GET: HocKis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HocKis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenHK")] HocKi hocKi)
        {
            if (ModelState.IsValid)
            {
                db.HocKis.Add(hocKi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hocKi);
        }

        // GET: HocKis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocKi hocKi = db.HocKis.Find(id);
            if (hocKi == null)
            {
                return HttpNotFound();
            }
            return View(hocKi);
        }

        // POST: HocKis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenHK")] HocKi hocKi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hocKi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hocKi);
        }

        // GET: HocKis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocKi hocKi = db.HocKis.Find(id);
            if (hocKi == null)
            {
                return HttpNotFound();
            }
            return View(hocKi);
        }

        // POST: HocKis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HocKi hocKi = db.HocKis.Find(id);
            db.HocKis.Remove(hocKi);
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
