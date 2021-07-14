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

namespace WebDangKyKHHT.Controllers
{
    public class MonHocsController : Controller
    {
        private SEP_TEAM15_WEBKHHTEntities db;
        private List<int> MonHoc;
        public MonHocsController()
        {
            db = new SEP_TEAM15_WEBKHHTEntities();
            MonHoc = new List<int>();
        }


        //[Authorize]
        // GET: DangKyKHHT
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

        [HttpPost]
        public JsonResult jsonMH(int? hk)
        {
            var dataMH = (from objMH in db.MonHocs
                          select new MonHocsViewModel()
                          {
                              IDMH = objMH.ID,
                              TenMH = objMH.TenMH,
                              MaMH = objMH.MaMH,
                              SoTinChi = (int)objMH.SoTinChi,
                              ID_HK = (int)objMH.ID_HK,
                              IsSelected = false
                          }).ToList();
            if (Session["monhoc"] != null)
            {
                MonHoc = (List<int>)Session["monhoc"];
            }

            if (MonHoc.Count > 0)
            {
                foreach (var dbrow in MonHoc)
                {
                    var findMH = dataMH.Where(m => m.IDMH == dbrow).FirstOrDefault();
                    if (findMH != null)
                        dataMH.Remove(findMH);
                }
            }
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string sText = Request["search[value]"].ToLower();
            int row = dataMH.Count();
            if (!string.IsNullOrEmpty(sText) && hk != null)
                dataMH = dataMH.Where(m => m.TenMH.ToLower().Contains(sText) && m.ID_HK == hk).ToList();
            else if (!string.IsNullOrEmpty(sText) && hk == null)
                dataMH = dataMH.Where(m => m.TenMH.ToLower().Contains(sText)).ToList();
            else if (string.IsNullOrEmpty(sText) && hk != null)
                dataMH = dataMH.Where(m => m.ID_HK == hk).ToList();
            int rowfilter = dataMH.Count();
            dataMH = dataMH.Skip(start).Take(length).ToList();
            return Json(new { data = dataMH, draw = Request["draw"], recordsTotal = row, recordsFiltered = rowfilter }, JsonRequestBehavior.AllowGet);
        }

        //private SEP_TEAM15_WEBKHHTEntities db = new SEP_TEAM15_WEBKHHTEntities();

        //// GET: MonHocs
        //[AllowAnonymous]
        //public ActionResult Index()
        //{
        //    var dlhk = db.HocKis.ToList();
        //    List<SelectListItem> idhk = new List<SelectListItem>();
        //    idhk.Add(new SelectListItem { Value = "", Text = "--Tất cả--", Selected = true });
        //    foreach (var item in dlhk)
        //    {
        //        idhk.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.TenHK.ToString() });
        //    }
        //    ViewBag.ID_HK = idhk;
        //    //ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID");
        //    var monHocs = db.MonHocs.Include(m => m.HocKi);
        //    return View(monHocs.ToList());

        //}
        //[HttpPost]
        //public ActionResult Index(int ID_HK)
        //{
        //    var dlhk = db.HocKis.ToList();
        //    List<SelectListItem> idhk = new List<SelectListItem>();
        //    idhk.Add(new SelectListItem { Value = "", Text = "--Tất cả--", Selected = true });
        //    foreach (var item in dlhk)
        //    {
        //        idhk.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.TenHK.ToString() });
        //    }
        //    ViewBag.ID_HK = idhk;
        //    //ViewBag.ID_HK = new SelectList(db.HocKis, "ID", "ID");
        //    var monHocs = db.MonHocs.Include(m => m.HocKi).Where(a => a.ID_HK == ID_HK);
        //    return View(monHocs.ToList());

        //}

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
