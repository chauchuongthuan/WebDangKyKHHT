using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDangKyKHHT.Models;
using WebDangKyKHHT.ViewModels;

namespace WebDangKyKHHT.Controllers
{
    public class DangKyKHHTController : Controller
    {
        private SEP_TEAM15_WEBKHHTEntities objSEP_TEAM15_WEBKHHTEntities;
        public DangKyKHHTController()
        {
            objSEP_TEAM15_WEBKHHTEntities = new SEP_TEAM15_WEBKHHTEntities();
        }
        // GET: DangKyKHHT
        public ActionResult Index()
        {
            //ViewBag.ID_HK = new SelectList(objSEP_TEAM15_WEBKHHTEntities.HocKis, "ID", "ID");            

            KHHTsViewModel objKHHTsViewModel = new KHHTsViewModel()
            {
                ListofSV = (from objSV in objSEP_TEAM15_WEBKHHTEntities.AspNetUsers
                            select new SelectListItem()
                            {
                                Text = objSV.UserName, 
                                Value = objSV.Id
                            }).ToList(),

                ListofMH = (from objMH in objSEP_TEAM15_WEBKHHTEntities.MonHocs
                            select new MonHocsViewModel()
                            {
                                IDMH = objMH.ID,
                                TenMH = objMH.TenMH,
                                MaMH = objMH.MaMH,
                                SoTinChi = (int)objMH.SoTinChi,
                                ID_HK = (int)objMH.ID_HK,
                                IsSelected = false
                            }).ToList()
            };
            return View(objKHHTsViewModel);
        }
        [HttpPost]
        public JsonResult Index(string ID_SV, List<int> listOfIDMH)
        {
            if (objSEP_TEAM15_WEBKHHTEntities.KHHTs.Any(model => model.ID_SV == ID_SV))
            {
                objSEP_TEAM15_WEBKHHTEntities.KHHTs.RemoveRange(objSEP_TEAM15_WEBKHHTEntities.KHHTs.Where(model => model.ID_SV == ID_SV));
                objSEP_TEAM15_WEBKHHTEntities.SaveChanges();
            }
            foreach (var item in listOfIDMH)
            {
                KHHT objKHHT = new KHHT();
                objKHHT.ID_SV = ID_SV;
                objKHHT.ID_MH = item;
                objKHHT.NgayTao = DateTime.Now;
                objSEP_TEAM15_WEBKHHTEntities.KHHTs.Add(objKHHT);
                objSEP_TEAM15_WEBKHHTEntities.SaveChanges();
            }
            return Json(new { success = true, message = "Đăng ký thành công"}, JsonRequestBehavior.AllowGet);
        }
        
    }

}