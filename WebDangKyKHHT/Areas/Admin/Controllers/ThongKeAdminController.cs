using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using WebDangKyKHHT.Models;



namespace WebDangKyKHHT.Areas.Admin.Controllers
{
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
            var thongke = model.KHHTs.OrderByDescending(x => x.ID).ToList();
            return View(thongke);
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