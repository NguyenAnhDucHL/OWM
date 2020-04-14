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
    [Authorize(Roles = "Admin,WebManager")]
    public class CategoryPostAdminController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Identity/CategoryPostAdmin
        public ActionResult Index()
        {
            return View(db.CategoryPosts.ToList());
        }

        // GET: Identity/CategoryPostAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPost categoryPost = db.CategoryPosts.Find(id);
            if (categoryPost == null)
            {
                return HttpNotFound();
            }
            return View(categoryPost);
        }

        // GET: Identity/CategoryPostAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Identity/CategoryPostAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryPostId,CategoryName")] CategoryPost categoryPost)
        {
            if (ModelState.IsValid)
            {
                db.CategoryPosts.Add(categoryPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryPost);
        }

        // GET: Identity/CategoryPostAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPost categoryPost = db.CategoryPosts.Find(id);
            if (categoryPost == null)
            {
                return HttpNotFound();
            }
            return View(categoryPost);
        }

        // POST: Identity/CategoryPostAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryPostId,CategoryName")] CategoryPost categoryPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryPost);
        }

        // GET: Identity/CategoryPostAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPost categoryPost = db.CategoryPosts.Find(id);
            if (categoryPost == null)
            {
                return HttpNotFound();
            }
            return View(categoryPost);
        }

        // POST: Identity/CategoryPostAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CategoryPost categoryPost = db.CategoryPosts.Find(id);
            db.CategoryPosts.Remove(categoryPost);
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
