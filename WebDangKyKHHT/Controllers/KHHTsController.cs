using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebDangKyKHHT.Models;

namespace WebDangKyKHHT.Controllers
{
    [Authorize]
    public class KHHTsController : Controller
    {
        private SEP_TEAM15_WEBKHHTEntities db = new SEP_TEAM15_WEBKHHTEntities();

        // GET: KHHTs
        public ActionResult Index()
        {
            var idUs = User.Identity.GetUserId();
            var kHHTs = db.KHHTs.Where(u => u.ID_SV == idUs).GroupBy(h => h.ID_HK).Select(i => i.Key).Cast<int>();
            var hk = db.HocKis.Where(h => kHHTs.Contains(h.ID)).ToList();
            return View(hk);
        }

        // GET: KHHTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var us = User.Identity.GetUserId();
            var mh = db.KHHTs.Where(n => n.ID_SV == us && n.ID_HK == id).Select(n => n.ID_MH).ToList();
            if (mh == null)
            {
                return HttpNotFound();
            }
            var data = db.MonHocs.Where(n => mh.Contains(n.ID)).ToList();
            ViewBag.NameUser = User.Identity.Name;
            ViewBag.HocKy = db.HocKis.Find(id);
            return View(data);
        }

        public ActionResult print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var us = User.Identity.GetUserId();
            var mh = db.KHHTs.Where(n => n.ID_SV == us && n.ID_HK == id).Select(n => n.ID_MH).ToList();
            var data = db.MonHocs.Where(n => mh.Contains(n.ID)).ToList();
            string NameUser = User.Identity.Name;
            var HocKy = db.HocKis.Find(id);

            var pck = new ExcelPackage(new FileInfo(Server.MapPath("~/assets/Dulieu.xlsx")));
            ExcelWorksheet ws = pck.Workbook.Worksheets[0];

            ws.Cells["D6"].Value = NameUser; // Họ tên
            //ws.Cells["F6"].Value = "Họ tên"; // Lớp
            //ws.Cells["D7"].Value = "Lớp"; //Khóa
            ws.Cells["F7"].Value = HocKy.TenHK; //Học kỳ
            int rowStart = 10, stt = 0;
            foreach (var item in data)
            {
                ws.Cells[string.Format("C{0}", rowStart)].Value = ++stt;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.TenMH;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.MaMH;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.SoTinChi;
                rowStart++;
            }
            var filedata = pck.GetAsByteArray();
            return File(filedata, "application/octet-stream", "Dulieu.xlsx");
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
