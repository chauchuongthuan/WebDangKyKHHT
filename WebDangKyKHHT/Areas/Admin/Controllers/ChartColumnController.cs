using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDangKyKHHT.Areas.Admin.Controllers
{
    public class ChartColumnController : Controller
    {
        // GET: Admin/ChartColumn
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult ChartC()
        {
            var sqlconnectionstring = @"data source=tuleap.vanlanguni.edu.vn,18082;initial catalog=SEP24Team15;user id=SEP24Team15;password=Qwerty123456;MultipleActiveResultSets=True;App=EntityFramework";
            var connnection = new SqlConnection(sqlconnectionstring);
            connnection.Open();
            var command = new SqlCommand();
            command.Connection = connnection;
            command.CommandText = "SELECT COUNT(DISTINCT ID_SV) FROM KHHT WHERE ID_HK = 1 ";
            var soluong = command.ExecuteScalar();
            connnection.Close();
            return Json(new { dbchart1 = soluong }, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public JsonResult ChartC1()
        {
            var sqlconnectionstring = @"data source=tuleap.vanlanguni.edu.vn,18082;initial catalog=SEP24Team15;user id=SEP24Team15;password=Qwerty123456;MultipleActiveResultSets=True;App=EntityFramework";
            var connnection1 = new SqlConnection(sqlconnectionstring);
            connnection1.Open();
            var command1 = new SqlCommand();
            command1.Connection = connnection1;
            command1.CommandText = "SELECT COUNT(DISTINCT ID_SV) FROM KHHT WHERE ID_HK = 2 ";
            var soluong1 = command1.ExecuteScalar();
            connnection1.Close();
            return Json(new { dbchart2 = soluong1 }, JsonRequestBehavior.AllowGet);
        }
    }
}