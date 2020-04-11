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
        public ProductController()
        {
            _productRepository = new ProductRepository();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(string productId)
        {
            var product = _productRepository.Find(productId);
            return View(product);
        }
    }
}