using OMW_Project.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMW_Project.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private IPostRepository _postRepository;
        public ProductController()
        {
            _productRepository = new ProductRepository();
            _postRepository = new PostRepository();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(string productId)
        {
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            var product = _productRepository.Find(productId);
            return View(product);
        }
    }
}