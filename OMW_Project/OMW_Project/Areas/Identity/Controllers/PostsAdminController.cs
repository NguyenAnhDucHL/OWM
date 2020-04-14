using Microsoft.AspNet.Identity;
using OMW_Project.Models;
using OMW_Project.Repositories;
using System.IO;
using System.Net;
using System.Web.Mvc;


namespace OMW_Project.Areas.Identity.Controllers
{
    [Authorize]
    public class PostsAdminController : Controller
    {
        private IPostRepository _postRepository;
        private ICategoryPostRepository _categoryPostRepository;
        public PostsAdminController()
        {
            _postRepository = new PostRepository();
            _categoryPostRepository = new CategoryPostRepository();
        }
        public ActionResult Home()
        {
            return View();
        }
        // GET: Identity/Posts
        public ActionResult Index()
        {
            ViewBag.Index = 1;
            ViewBag.View = "Danh sách bài viết";
            return View(_postRepository.GetAll());
        }
        public ActionResult PostApproved()
        {
            ViewBag.View = "Danh sách bài viết đã kiểm duyệt";
            ViewBag.Index = 0;
            return View("Index",_postRepository.GetApproved());
        }
        public ActionResult PostUnApproved()
        {
            ViewBag.View = "Danh sách bài viết chưa kiểm duyệt";
            ViewBag.Index = 0;
            var x = _postRepository.GetUnApproved();
            return View("Index", _postRepository.GetUnApproved());
        }

        // GET: Identity/Posts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _postRepository.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Identity/Posts/Create
        public ActionResult Create()
        {

            var x= new SelectList(_categoryPostRepository.GetAll(), "CategoryPostId", "CategoryName");
            ViewBag.CategoryPostId = new SelectList(_categoryPostRepository.GetAll(), "CategoryPostId", "CategoryName");
            return View();
        }

        // POST: Identity/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
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

        // GET: Identity/Posts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _postRepository.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryPostId = new SelectList(_categoryPostRepository.GetAll(), "CategoryPostId", "CategoryName", post.CategoryPostId);
            return View(post);
        }

        // POST: Identity/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
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
                _postRepository.Update(post);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryPostId = new SelectList(_categoryPostRepository.GetAll(), "CategoryPostId", "CategoryName", post.CategoryPostId);
            return View(post);
        }

        // GET: Identity/Posts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _postRepository.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Identity/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            _postRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
