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
    public class KHHTsController : Controller
    {
        private SEP_TEAM15_DKKKHHTEntities db = new SEP_TEAM15_DKKKHHTEntities();

        // GET: KHHTs
        public ActionResult Index()
        {
            var kHHTs = db.KHHTs.Include(k => k.HocKi).Include(k => k.MonHoc);
            return View(kHHTs.ToList());
        }

        // GET: KHHTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHHT kHHT = db.KHHTs.Find(id);
            if (kHHT == null)
            {
                return HttpNotFound();
            }
            return View(kHHT);
        }

        // GET: KHHTs/Create
        public ActionResult Create()
        {
            ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID");
            ViewBag.ID_MH = new SelectList(db.MonHocs, "ID", "MaMH");
            return View();
        }

        // POST: KHHTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_MH,ID_HK,NgayTao,NutTick")] KHHT kHHT)
        {
            if (ModelState.IsValid)
            {
                db.KHHTs.Add(kHHT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID", kHHT.ID_HK);
            ViewBag.ID_MH = new SelectList(db.MonHocs, "ID", "MaMH", kHHT.ID_MH);
            return View(kHHT);
        }

        // GET: KHHTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHHT kHHT = db.KHHTs.Find(id);
            if (kHHT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID", kHHT.ID_HK);
            ViewBag.ID_MH = new SelectList(db.MonHocs, "ID", "MaMH", kHHT.ID_MH);
            return View(kHHT);
        }

        // POST: KHHTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_MH,ID_HK,NgayTao,NutTick")] KHHT kHHT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHHT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID", kHHT.ID_HK);
            ViewBag.ID_MH = new SelectList(db.MonHocs, "ID", "MaMH", kHHT.ID_MH);
            return View(kHHT);
        }

        // GET: KHHTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHHT kHHT = db.KHHTs.Find(id);
            if (kHHT == null)
            {
                return HttpNotFound();
            }
            return View(kHHT);
        }

        // POST: KHHTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHHT kHHT = db.KHHTs.Find(id);
            db.KHHTs.Remove(kHHT);
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
