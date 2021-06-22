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
    public class MonHocsController : Controller
    {
        private SEP_TEAM15_WEBKHHTEntities db = new SEP_TEAM15_WEBKHHTEntities();

        // GET: MonHocs
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID");
            var monHocs = db.MonHocs.Include(m => m.HocKi);
            return View(monHocs.ToList());
            
        }
        [HttpPost]
        public ActionResult Index(int ID_HK)
        {
            ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID");
            var monHocs = db.MonHocs.Include(m => m.HocKi).Where(a => a.ID_HK == ID_HK);
            return View(monHocs.ToList());

        }

        // GET: MonHocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonHoc monHoc = db.MonHocs.Find(id);
            if (monHoc == null)
            {
                return HttpNotFound();
            }
            return View(monHoc);
        }

        // GET: MonHocs/Create
        public ActionResult Create()
        {
            ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID");
            return View();
        }

        // POST: MonHocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaMH,TenMH,SoTinChi,ID_HK")] MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                db.MonHocs.Add(monHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID", monHoc.ID_HK);
            return View(monHoc);
        }

        // GET: MonHocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonHoc monHoc = db.MonHocs.Find(id);
            if (monHoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID", monHoc.ID_HK);
            return View(monHoc);
        }

        // POST: MonHocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaMH,TenMH,SoTinChi,ID_HK")] MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID", monHoc.ID_HK);
            return View(monHoc);
        }

        // GET: MonHocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonHoc monHoc = db.MonHocs.Find(id);
            if (monHoc == null)
            {
                return HttpNotFound();
            }
            return View(monHoc);
        }

        // POST: MonHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonHoc monHoc = db.MonHocs.Find(id);
            db.MonHocs.Remove(monHoc);
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


        [AllowAnonymous]
        public ActionResult Search(string HK)
        {
            var monhoc = db.MonHocs.ToList();           
            int iHK = int.Parse(HK);
            monhoc = monhoc.Where(m => m.HocKi.TenHK == iHK).ToList();
            return View("Index", monhoc);
        }
    }
}
