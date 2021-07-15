using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDangKyKHHT.Models;

namespace WebDangKyKHHT.Areas.Admin.Controllers
{
    public class BCNKhoasController : Controller
    {
        private SEP_TEAM15_WEBKHHTEntities db = new SEP_TEAM15_WEBKHHTEntities();

        // GET: Admin/BCNKhoas
        public ActionResult Index()
        {
            return View(db.BCNKhoas.ToList());
        }

        // GET: Admin/BCNKhoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BCNKhoa bCNKhoa = db.BCNKhoas.Find(id);
            if (bCNKhoa == null)
            {
                return HttpNotFound();
            }
            return View(bCNKhoa);
        }

        // GET: Admin/BCNKhoas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BCNKhoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenTK,MatKhau,TenBCN")] BCNKhoa bCNKhoa)
        {
            if (ModelState.IsValid)
            {
                db.BCNKhoas.Add(bCNKhoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bCNKhoa);
        }

        // GET: Admin/BCNKhoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BCNKhoa bCNKhoa = db.BCNKhoas.Find(id);
            if (bCNKhoa == null)
            {
                return HttpNotFound();
            }
            return View(bCNKhoa);
        }

        // POST: Admin/BCNKhoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenTK,MatKhau,TenBCN")] BCNKhoa bCNKhoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bCNKhoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bCNKhoa);
        }

        // GET: Admin/BCNKhoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BCNKhoa bCNKhoa = db.BCNKhoas.Find(id);
            if (bCNKhoa == null)
            {
                return HttpNotFound();
            }
            return View(bCNKhoa);
        }

        // POST: Admin/BCNKhoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BCNKhoa bCNKhoa = db.BCNKhoas.Find(id);
            db.BCNKhoas.Remove(bCNKhoa);
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
