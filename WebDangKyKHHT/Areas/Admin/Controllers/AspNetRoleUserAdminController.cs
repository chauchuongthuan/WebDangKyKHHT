using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDangKyKHHT.Models;

namespace WebDangKyKHHT.Areas.Admin.Controllers
{
    public class AspNetRoleUserAdminController : Controller
    {
        private SEP_TEAM15_WEBKHHTEntities db = new SEP_TEAM15_WEBKHHTEntities();
        // GET: AspNetRoleUserAdmin/Create
        public ActionResult Create(string roleId)
        {
            ViewBag.Role = db.AspNetRoles.Find(roleId);
            ViewBag.Users = new SelectList(db.AspNetUsers, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string roleId, string userId)
        {
            var role = db.AspNetRoles.Find(roleId);
            var user = db.AspNetUsers.Find(userId);

            role.AspNetUsers.Add(user);
            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "AspNetRolesAdmin");
        }


        // GET: AspNetRolesAdmin/Delete/5
        public ActionResult Delete(string roleId, string userId)
        {
            var role = db.AspNetRoles.Find(roleId);
            var user = db.AspNetUsers.Find(userId);

            role.AspNetUsers.Remove(user);
            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "AspNetRolesAdmin");
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