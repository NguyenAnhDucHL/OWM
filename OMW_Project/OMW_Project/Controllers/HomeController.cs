using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OMW_Project.Models;
using OMW_Project.Repositories;
using OMW_Project.ViewModels;

namespace OMW_Project.Controllers
{
    public class HomeController : Controller
    {
        private IPostRepository _postRepository;
        private ICategoryPostRepository _categoryPostRepository;
        private ICategoryProductRepository _categoryProductRepository;
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        private IUserRepository _userRepository;
        public HomeController()
        {
            _postRepository = new PostRepository();
            _categoryPostRepository = new CategoryPostRepository();
            _categoryProductRepository = new CategoryProductRepository();
            _productRepository = new ProductRepository();
            _orderRepository = new OrderRepository();
            _userRepository = new UserRepository();
        }
        public ActionResult Index()
        {
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            return View(_postRepository.GetAll());
        }

        public ActionResult CategoryDetail(string @id)
        {
            ViewBag.getCategoryDetail = _postRepository.GetDetail_Category(id);
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Store(string categoryId = "")
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.CateList = _categoryProductRepository.GetAll();
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            IList<Product> products = null;
            if (string.IsNullOrEmpty(categoryId))
            {
                products = _productRepository.GetAll();
            }
            else
            {
                products = _productRepository.GetByCategory(categoryId);
            }
            return View(products);
        }
        [Authorize]
        [HttpPost]
        public ActionResult SaveOrder(OrdersViewModel model)
        {
            var product = (from p in model.ListProducts
                           select new OrderProduct
                           {
                               ProductId = p.ProductId,
                               Quantity = p.Quantity,
                               UnitCost = p.UnitCost,
                               TotalCost = p.Quantity * p.UnitCost
                           }).ToList();
            _orderRepository.Add(new Order()
            {
                Address = model.Address,
                Email = model.Email,
                FullName = model.FullName,
                Mobile = model.Mobile,
                OrderProducts = product,
                TotalCost = (double)product.Sum(p => p.TotalCost),
                UserId = User.Identity.GetUserId()

            });
            return Json(new { IsSuccess = true }); ;
        }
        [Authorize]
        public ActionResult Order()
        {
            var user = _userRepository.Find(User.Identity.GetUserId());
            OrdersViewModel model = new OrdersViewModel()
            {
                Address= user.Address,
                Email= user.Email,
                FullName= user.FullName,
                Mobile= user.PhoneNumber
            };
            return View(model);
        }
        [Authorize]
        public ActionResult HistoryOrder()
        {
            var userId = User.Identity.GetUserId();
            var data = _orderRepository.GetHistoryByPatient(userId);
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            return View(data);
        }

        [HttpGet]
        public ActionResult PostDetail(string @id)
        {
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            ViewBag.PostDetail = _postRepository.GetPostDetail(id);
            return View();
        }
    }
}