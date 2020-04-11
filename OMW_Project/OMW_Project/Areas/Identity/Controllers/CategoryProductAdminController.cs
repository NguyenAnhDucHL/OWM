using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OMW_Project.Models;

namespace OMW_Project.Areas.Identity.Controllers
{
    public class CategoryProductAdminController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Identity/CategoryProductAdmin
        public ActionResult Index()
        {
            return View(db.CategoryProducts.ToList());
        }

        // GET: Identity/CategoryProductAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProduct categoryProduct = db.CategoryProducts.Find(id);
            if (categoryProduct == null)
            {
                return HttpNotFound();
            }
            return View(categoryProduct);
        }

        // GET: Identity/CategoryProductAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Identity/CategoryProductAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryProductId,CategoryName")] CategoryProduct categoryProduct)
        {
            if (ModelState.IsValid)
            {
                db.CategoryProducts.Add(categoryProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryProduct);
        }

        // GET: Identity/CategoryProductAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProduct categoryProduct = db.CategoryProducts.Find(id);
            if (categoryProduct == null)
            {
                return HttpNotFound();
            }
            return View(categoryProduct);
        }

        // POST: Identity/CategoryProductAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryProductId,CategoryName")] CategoryProduct categoryProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryProduct);
        }

        // GET: Identity/CategoryProductAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProduct categoryProduct = db.CategoryProducts.Find(id);
            if (categoryProduct == null)
            {
                return HttpNotFound();
            }
            return View(categoryProduct);
        }

        // POST: Identity/CategoryProductAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CategoryProduct categoryProduct = db.CategoryProducts.Find(id);
            db.CategoryProducts.Remove(categoryProduct);
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
