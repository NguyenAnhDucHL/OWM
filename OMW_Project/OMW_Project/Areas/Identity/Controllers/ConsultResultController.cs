using Microsoft.AspNet.Identity;
using OMW_Project.Models;
using OMW_Project.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMW_Project.Areas.Identity.Controllers
{
    [Authorize]
    public class ConsultResultController : Controller
    {
        // GET: Identity/ConsultResult
        private IConsultResultRepository _consultResultRepository;
        private IConsultingRepository _consultingRepository;
        private IProductRepository _productRepository;

        public ConsultResultController()
        {
            _consultResultRepository = new ConsultResultRepository();
            _consultingRepository = new ConsultingRepository();
            _productRepository = new ProductRepository();
        }
        [Authorize(Roles ="Doctor")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var data = _consultingRepository.GetAllMetByDoctorIdResult(userId);
            return View(data);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create(string id)
        {
            var consulting = _consultingRepository.Find(id);
            ViewBag.Products = _productRepository.GetAll();
            return View(consulting);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(ConsultResult consultResult)
        {
            _consultResultRepository.Add(consultResult);
            if (true)
            {
                return Json(new { IsSuccess = true });
            }
            return View();
        }
    }
}