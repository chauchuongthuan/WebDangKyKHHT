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
            command.CommandText = "SELECT COUNT(DISTINCT ID_SV) FROM KHHT ";
            var soluong = command.ExecuteScalar();
            connnection.Close();
            return Json(new { dbchart1 = soluong }, JsonRequestBehavior.AllowGet);
        }
    }
}