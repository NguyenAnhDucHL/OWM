using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OMW_Project.Models;
using OMW_Project.Repositories;

namespace OMW_Project.Areas.Identity.Controllers
{
    public class PostsDoctorController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        private IPostRepository _postRepository;
        private ICategoryPostRepository _categoryPostRepository;

        public PostsDoctorController()
        {
            _postRepository = new PostRepository();
            _categoryPostRepository = new CategoryPostRepository();
        }
        // GET: Identity/PostsDoctor
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var posts = db.Posts.Include(p => p.CategoryPost).Include(p => p.User).Where(c=>c.UserId == userId);
            return View(posts.ToList());
        }

        // GET: Identity/PostsDoctor/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Identity/PostsDoctor/Create
        public ActionResult Create()
        {
            ViewBag.CategoryPostId = new SelectList(db.CategoryPosts, "CategoryPostId", "CategoryName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: Identity/PostsDoctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,UserId,Title,Description,Image,Status,CategoryPostId")] Post post)
        {
            if (post.myfile != null && post.myfile.ContentLength > 0)
            {
                string imgName = Path.GetFileName(post.myfile.FileName);
                string imgExt = Path.GetExtension(imgName);
                if (imgExt.Equals(".jpg") || imgExt.Equals(".jpeg") || imgExt.Equals(".png"))
                {
                    string imgPath = Path.Combine(Server.MapPath("~/Assets/img"), imgName);
                    post.myfile.SaveAs(imgPath);
                    post.Image = "/Assets/img/" + imgName;
                }
                else
                {
                    ModelState.AddModelError("", "Wrong file type");
                }
            }
            if (ModelState.IsValid)
            {
                post.UserId = User.Identity.GetUserId();
                _postRepository.Add(post);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryPostId = new SelectList(_categoryPostRepository.GetAll(), "CategoryPostId", "CategoryName", post.CategoryPostId);
            return View(post);
        }

        // GET: Identity/PostsDoctor/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryPostId = new SelectList(db.CategoryPosts, "CategoryPostId", "CategoryName", post.CategoryPostId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", post.UserId);
            return View(post);
        }

        // POST: Identity/PostsDoctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,UserId,Title,Description,Image,Status,CategoryPostId")] Post post)
        {
            if (post.myfile != null && post.myfile.ContentLength > 0)
            {
                string imgName = Path.GetFileName(post.myfile.FileName);
                string imgExt = Path.GetExtension(imgName);
                if (imgExt.Equals(".jpg") || imgExt.Equals(".jpeg") || imgExt.Equals(".png"))
                {
                    string imgPath = Path.Combine(Server.MapPath("~/Assets/img"), imgName);
                    post.myfile.SaveAs(imgPath);
                    post.Image = "/Assets/img/" + imgName;
                }
                else
                {
                    ModelState.AddModelError("", "Wrong file type");
                }
            }
            if (ModelState.IsValid)
            {
                post.UserId = User.Identity.GetUserId();
                _postRepository.Update(post);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryPostId = new SelectList(_categoryPostRepository.GetAll(), "CategoryPostId", "CategoryName", post.CategoryPostId);
            return View(post);
        }

        // GET: Identity/PostsDoctor/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Identity/PostsDoctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
