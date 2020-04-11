using OMW_Project.Models;
using OMW_Project.Repositories;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace OMW_Project.Areas.Identity.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProductsAdminController : Controller
    {
        private IProductRepository _productRepository;
        private ICategoryProductRepository _categoryProductRepository;
        public ProductsAdminController()
        {
            _productRepository = new ProductRepository();
            _categoryProductRepository = new CategoryProductRepository();
        }
        // GET: Identity/Products
        public ActionResult Index()
        {
            return View(_productRepository.GetAll());
        }

        // GET: Identity/Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productRepository.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Identity/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_categoryProductRepository.GetAll(), "CategoryProductId", "CategoryName");
            return View();
        }

        // POST: Identity/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product)
        {
            if (product.myfile != null && product.myfile.ContentLength > 0)
            {
                string imgName = Path.GetFileName(product.myfile.FileName);
                string imgExt = Path.GetExtension(imgName);
                if(imgExt.Equals(".jpg")|| imgExt.Equals(".jpeg") || imgExt.Equals(".png"))
                {
                    string imgPath = Path.Combine(Server.MapPath("~/Assets/img"), imgName);
                    product.myfile.SaveAs(imgPath);
                    product.Image = "/Assets/img/"+ imgName;
                }else
                {
                    ModelState.AddModelError("", "Wrong file type");
                }
            }
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_categoryProductRepository.GetAll(), "CategoryProductId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Identity/Products/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productRepository.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_categoryProductRepository.GetAll(), "CategoryProductId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Identity/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (product.myfile != null && product.myfile.ContentLength > 0)
            {
                string imgName = Path.GetFileName(product.myfile.FileName);
                string imgExt = Path.GetExtension(imgName);
                if (imgExt.Equals(".jpg") || imgExt.Equals(".jpeg") || imgExt.Equals(".png"))
                {
                    string imgPath = Path.Combine(Server.MapPath("~/Assets/img"), imgName);
                    product.myfile.SaveAs(imgPath);
                    product.Image = "/Assets/img/" + imgName;
                }
                else
                {
                    ModelState.AddModelError("", "Wrong file type");
                }
            }
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_categoryProductRepository.GetAll(), "CategoryProductId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Identity/Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productRepository.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Identity/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            _productRepository.Remove(id);
            return RedirectToAction("Index");
        }

      
    }
}
