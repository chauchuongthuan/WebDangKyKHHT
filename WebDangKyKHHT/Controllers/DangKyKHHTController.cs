using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebDangKyKHHT.Models;
using WebDangKyKHHT.ViewModels;



namespace WebDangKyKHHT.Controllers
{

    public class DangKyKHHTController : Controller
    {
        private SEP_TEAM15_WEBKHHTEntities db;
        private List<int> MonHoc;
        public DangKyKHHTController()
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

        [HttpPost]
        public JsonResult addKhht(int idMH)
        {
            if (Session["monhoc"] != null)
                MonHoc = (List<int>)Session["monhoc"];
            MonHoc.Add(idMH);
            Session["monhoc"] = MonHoc;
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit()
        {
            var dlhk = db.HocKis.ToList();
            List<SelectListItem> idhk = new List<SelectListItem>();
            idhk.Add(new SelectListItem { Value = "", Text = "Chọn học kỳ", Selected = true });
            foreach (var item in dlhk)
            {
                idhk.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.TenHK.ToString() });
            }
            ViewBag.ID_HK = idhk;

            if (Session["monhoc"] != null)
                MonHoc = (List<int>)Session["monhoc"];
            var vdb = GetMH(MonHoc);
            return View(vdb);
        }

        [HttpPost]
        public JsonResult removeMH(int? MaMH)
        {
            try
            {
                if (MaMH != null)
                {
                    if (Session["monhoc"] != null)
                        MonHoc = (List<int>)Session["monhoc"];
                    bool result = MonHoc.Remove((int)MaMH);
                    if (result)
                    {
                        Session["monhoc"] = MonHoc;
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch { }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit(List<int> MaMH, int? hk)
        {
            try
            {
                if (hk == null)
                {
                    TempData["notify"] = "<div class=\"alert alert-danger\" role=\"alert\">Bạn chưa chọn học kỳ</div>";
                    return RedirectToAction("Edit");
                }
                string ID_SV = User.Identity.GetUserId();
                if (MaMH != null)
                {
                    if (MaMH.Count > 0)
                    {
                        var datatemp = GetMH(MaMH);
                        int tSoTinChi = 0;
                        foreach (var dl in datatemp)
                        {
                            tSoTinChi += dl.SoTinChi;
                        }
                        if (tSoTinChi >= 12 && tSoTinChi <= 20)
                        {
                            db.KHHTs.RemoveRange(db.KHHTs.Where(m => m.ID_SV == ID_SV && m.ID_HK == hk));
                            foreach (var item in MaMH)
                            {
                                KHHT objKHHT = new KHHT();
                                objKHHT.ID_SV = ID_SV;
                                objKHHT.ID_MH = item;
                                objKHHT.NgayTao = DateTime.Now;
                                objKHHT.ID_HK = hk;
                                db.KHHTs.Add(objKHHT);
                            }
                            db.SaveChanges();
                            Session["monhoc"] = null;
                            TempData["notify"] = "<div class=\"alert alert-success\" role=\"alert\">Lưu dữ liệu thành công</div>";
                        }
                        else
                            TempData["notify"] = "<div class=\"alert alert-danger\" role=\"alert\">Tổng số tín phải trong khoảng 12 đến 20 tín chỉ</div>";
                    }
                }
                else
                {
                    var objKHHT = db.KHHTs.RemoveRange(db.KHHTs.Where(m => m.ID_SV == ID_SV && m.ID_HK == hk));
                    db.SaveChanges();
                    Session["monhoc"] = null;
                    TempData["notify"] = "<div class=\"alert alert-danger\" role=\"alert\">KHHT của học kì bạn chọn đã bị xóa</div>";
                }
            }
            catch
            {
                TempData["notify"] = "<div class=\"alert alert-danger\" role=\"alert\">Lỗi dữ liệu vui lòng kiểm tra lại</div>";
            }
            return RedirectToAction("Edit");
        }

        protected List<MonHocsViewModel> GetMH(List<int> idMH)
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
            List<MonHocsViewModel> datatemp = new List<MonHocsViewModel>();
            for (int i = 0; i < dataMH.Count; i++)
            {
                foreach (var ma in idMH)
                {
                    if (dataMH[i].IDMH == ma)
                        datatemp.Add(dataMH[i]);
                }
            }
            return datatemp;
        }
    }
}