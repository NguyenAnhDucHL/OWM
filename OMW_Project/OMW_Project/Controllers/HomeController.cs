using System;
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
        private ICategoryProductRepository _categoryProductRepository;
        private IDoctorPaymentRepository _doctorPaymentRepository;
        private ICategoryPostRepository _categoryPostRepository;
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        private IPostRepository _postRepository;
        private IUserRepository _userRepository;
        private IDoctorContributionRepository _contributionRepository;
        
        public HomeController()
        {
            _postRepository = new PostRepository();
            _categoryPostRepository = new CategoryPostRepository();
            _categoryProductRepository = new CategoryProductRepository();
            _doctorPaymentRepository = new DoctorPaymentRepository();
            _productRepository = new ProductRepository();
            _orderRepository = new OrderRepository();
            _userRepository = new UserRepository();
            _contributionRepository= new DoctorContributionRepository();
        }
        public ActionResult Index()
        {
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            ViewBag.lstProducts = _productRepository.GetRandomProduct();
            return View(_postRepository.GetAll());
        }

        public ActionResult CategoryDetail(string @id)
        {
            ViewBag.CategoryName = _categoryPostRepository.Find(id).CategoryName;
            ViewBag.getCategoryDetail = _postRepository.GetDetail_Category(id);
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            return View();
        }
        [RequireAuthorizeFront(Roles ="User")]
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
                ViewBag.CategoryName = "Tất cả sản phẩm";
                products = _productRepository.GetAll();
            }
            else
            {
                products = _productRepository.GetByCategory(categoryId);
                var category = _categoryProductRepository.Find(categoryId);
                ViewBag.CategoryName = category.CategoryName;
            }
            return View(products);
        }

        public ActionResult Product()
        {
            ViewBag.CateList = _categoryProductRepository.GetAll();
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            var listProduct = _productRepository.GetAll();
            List<Product> list4Product = new List<Product>();
            int count = 0;
            foreach (var item in listProduct)
            {
                count++;
                if (count <= 4)
                {
                    list4Product.Add(item);
                }
                else
                {
                    break;
                }
            }

            ViewBag.list4Product = list4Product;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult SaveOrderWithDoctor(OrdersViewModel model)
        {
            var sumOriginPrice = 0;
            var sumSalePrice = 0;
            var docpercent = 0.0;
            foreach (var item in model.ListProducts)
            {
                var pro = _productRepository.Find(item.ProductId);
                sumOriginPrice += item.Quantity * pro.OriginPrice;
                sumSalePrice += item.Quantity * pro.SalePrice;
            }
            if(sumSalePrice<1000000)
            {
                docpercent = 0.05;
            }
            else if(sumSalePrice<5000000)
            {
                docpercent = 0.08;
            }else if(sumSalePrice<=10000000)
            {
                docpercent = 0.1;
            }
            else
            {
                docpercent = 0.15;
            }

            var docPay = (sumSalePrice - sumOriginPrice) * docpercent;
            DoctorPayment doctorPayment = new DoctorPayment()
            {
                ConsolidateTime = DateTime.Now,
                DoctorId = model.DocId,
                TotalPaid = (decimal) docPay
            };
            _doctorPaymentRepository.Add(doctorPayment);
            var product = (from p in model.ListProducts
                           select new OrderProduct
                           {
                               ProductId = p.ProductId,
                               Quantity = p.Quantity,
                               UnitCost = p.UnitCost,
                               TotalCost = p.Quantity * p.UnitCost
                           }).ToList();
            Order order = new Order()
            {
                Address = model.Address,
                Email = model.Email,
                FullName = model.FullName,
                Mobile = model.Mobile,
                OrderProducts = product,
                TotalCost = (double) product.Sum(p => p.TotalCost),
                UserId = User.Identity.GetUserId(),
                DoctorId = model.DocId

            };
            _orderRepository.Add(order);
            _contributionRepository.Add(new DoctorContribution()
            {
                OrderId = order.OrderId,
                DoctorPaymentId = doctorPayment.DoctorPaymentId,
                CommissionRate = (decimal)docpercent*100,
                CommissionAmount = (decimal)docPay
            });
            return Json(new { IsSuccess = true }); ;
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
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            var user = _userRepository.Find(User.Identity.GetUserId());
            ViewBag.lstCatePost = _postRepository.GetPost_Category();

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
        public ActionResult OrderWithDoctor(OrdersViewModel model)
        {
            var user = _userRepository.Find(User.Identity.GetUserId());
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            model.Address = user.Address;
            model.Email = user.Email;
            model.FullName = user.FullName;
            model.Mobile = user.PhoneNumber;
            return PartialView(model);
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