using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using WebDangKyKHHT.Models;



namespace WebDangKyKHHT.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ThongKeAdminController : Controller
    {
        SEP_TEAM15_WEBKHHTEntities model = new SEP_TEAM15_WEBKHHTEntities();
        // GET: ThongKe
        public ActionResult ThongKeChiTiet()
        {
            var thongkechitiet = model.KHHTs.OrderByDescending(x => x.ID).ToList();
            return View(thongkechitiet);
        } 
        public ActionResult Print(int id)
        {
            var printData = model.KHHTs.FirstOrDefault(x => x.ID == id);
            return View(printData);
        }
        public ActionResult Index()
        {
            var dlhk = model.HocKis.ToList();
            List<SelectListItem> idhk = new List<SelectListItem>();
            idhk.Add(new SelectListItem { Value = "", Text = "--Chọn học kì để xem biểu đồ--", Selected = false });
            foreach (var item in dlhk)
            {
                idhk.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.TenHK.ToString() });
            }
            ViewBag.ID_HK = idhk;
            //ViewBag.ID_HK = new SelectList(model.HocKis, "ID", "ID");
            var thongke = model.KHHTs.OrderByDescending(x => x.ID).ToList();
            return View(thongke);
        }
        [HttpPost]
        //Biểu đồ
        public JsonResult ChartData(int hk)
        {
            var noidung = model.KHHTs.Where(item => item.ID_HK == hk).GroupBy(item1 => item1.MonHoc.TenMH).Select(item2 => new { name = item2.Key, count = item2.Count()});
            return Json(new { dbchart = noidung }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChartC(int hk)
        {
            var sqlconnectionstring = @"data source=tuleap.vanlanguni.edu.vn,18082;initial catalog=SEP24Team15;user id=SEP24Team15;password=Qwerty123456";
            var connnection = new SqlConnection(sqlconnectionstring);
            connnection.Open();
            var command = new SqlCommand();
            command.Connection = connnection;
            command.CommandText = "SELECT COUNT(DISTINCT ID_SV) FROM KHHT WHERE ID_HK = @HK";
            var vHK = new SqlParameter("@HK", hk);
            command.Parameters.Add(vHK);
            var soluong = command.ExecuteScalar();
            connnection.Close();
            return Json(new { dbchart1 = soluong }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult listofExcel()
        {
            List<ThongKeViewModel> khhtList = model.KHHTs.Select(x => new ThongKeViewModel
            {
               
                ID_MH = x.MonHoc.TenMH,               
                ID_SV = x.ID_SV,
                NgayTao = x.NgayTao
            }).ToList();
            return View(khhtList);
        }
        public void ExportToExcel()
        {
            List<ThongKeViewModel> khhtList = model.KHHTs.Select(x => new ThongKeViewModel
            {

                ID_MH = x.MonHoc.TenMH,
                ID_SV = x.AspNetUser.Email,
                NgayTao = x.NgayTao
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Bảng thống kê";
            

            ws.Cells["A2"].Value = "Khoa";
            ws.Cells["B2"].Value = "Công nghệ thông tin";

            
            ws.Cells["A3"].Value = "Ngày in";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} lúc {0:H: mm tt}",DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Tên môn học";
            ws.Cells["B6"].Value = "Mã sinh viên";
            ws.Cells["C6"].Value = "Ngày tạo";

            int rowStart = 7;
            foreach(var item in khhtList)
            {                
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.ID_MH;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.ID_SV;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.NgayTao.ToString();
                rowStart++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename =" + "BangThongKe.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }    
    }
}