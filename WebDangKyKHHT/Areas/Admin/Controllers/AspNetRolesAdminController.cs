﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDangKyKHHT.Models;

namespace WebDangKyKHHT.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AspNetRolesAdminController : Controller
    {
        private SEP_TEAM15_WEBKHHTEntities db = new SEP_TEAM15_WEBKHHTEntities();

        // GET: Admin/AspNetRolesAdmin
        public ActionResult Index()
        {
            return View(db.AspNetRoles.ToList());
        }

        // GET: Admin/AspNetRolesAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRole aspNetRole = db.AspNetRoles.Find(id);
            if (aspNetRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetRole);
        }

        // GET: Admin/AspNetRolesAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AspNetRolesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AspNetRole aspNetRole)
        {
            if (ModelState.IsValid)
            {
                db.AspNetRoles.Add(aspNetRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetRole);
        }

        // GET: Admin/AspNetRolesAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRole aspNetRole = db.AspNetRoles.Find(id);
            if (aspNetRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetRole);
        }

        // POST: Admin/AspNetRolesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] AspNetRole aspNetRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetRole);
        }

        // GET: Admin/AspNetRolesAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRole aspNetRole = db.AspNetRoles.Find(id);
            if (aspNetRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetRole);
        }

        // POST: Admin/AspNetRolesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetRole aspNetRole = db.AspNetRoles.Find(id);
            db.AspNetRoles.Remove(aspNetRole);
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
