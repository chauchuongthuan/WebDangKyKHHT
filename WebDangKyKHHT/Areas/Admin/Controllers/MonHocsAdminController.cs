using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDangKyKHHT.Models;
using WebDangKyKHHT.ViewModels;

namespace WebDangKyKHHT.Areas.Admin.Controllers
{
    public class MonHocsAdminController : Controller
    {
        private SEP_TEAM15_WEBKHHTEntities db = new SEP_TEAM15_WEBKHHTEntities();

        // GET: Admin/MonHocsAdmin
        public ActionResult Index()
        {
            var dlhk = db.HocKis.ToList();
            List<SelectListItem> idhk = new List<SelectListItem>();
            idhk.Add(new SelectListItem { Value = "", Text = "--Tất cả--", Selected = true });
            foreach (var item in dlhk)
            {
                idhk.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.TenHK.ToString() });
            }
            ViewBag.ID_HK = idhk;
            return View();

        }
        public JsonResult jsonMH(int? hk)
        {
            var data = (from objMH in db.MonHocs
                        select new ViewMonHoc()
                        {
                            IDMH = objMH.ID,
                            TenMH = objMH.TenMH,
                            MaMH = objMH.MaMH,
                            SoTinChi = (int)objMH.SoTinChi,
                            ID_HK = (int)objMH.ID_HK,
                            TenHK = (int)objMH.HocKi.TenHK
                        }).ToList();
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string sText = Request["search[value]"].ToLower();
            int row = data.Count();
            if (!string.IsNullOrEmpty(sText) && hk != null)
                data = data.Where(m => m.TenMH.ToLower().Contains(sText) && m.ID_HK == hk).ToList();
            else if (!string.IsNullOrEmpty(sText) && hk == null)
                data = data.Where(m => m.TenMH.ToLower().Contains(sText)).ToList();
            else if (string.IsNullOrEmpty(sText) && hk != null)
                data = data.Where(m => m.ID_HK == hk).ToList();
            int rowfilter = data.Count();
            data = data.Skip(start).Take(length).ToList();
            return Json(new { data = data, draw = Request["draw"], recordsTotal = row, recordsFiltered = rowfilter }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/MonHocsAdmin/Details/5
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

        // GET: Admin/MonHocsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID");
            return View();
        }

        // POST: Admin/MonHocsAdmin/Create
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

        // GET: Admin/MonHocsAdmin/Edit/5
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

        // POST: Admin/MonHocsAdmin/Edit/5
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

        // GET: Admin/MonHocsAdmin/Delete/5
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

        // POST: Admin/MonHocsAdmin/Delete/5
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
    }
}
