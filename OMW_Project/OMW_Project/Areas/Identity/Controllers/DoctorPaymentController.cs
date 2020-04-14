using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OMW_Project.Models;
using OMW_Project.Repositories;

namespace OMW_Project.Areas.Identity.Controllers
{
    public class DoctorPaymentController : Controller
    {
        private IDoctorPaymentRepository _doctorPaymentRepository;
        private IDoctorContributionRepository _contributionRepository ;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;

        public DoctorPaymentController()
        {
            _doctorPaymentRepository = new DoctorPaymentRepository();
            _orderRepository = new OrderRepository();
            _productRepository = new ProductRepository();
            _contributionRepository= new DoctorContributionRepository();
        }
        // GET: Identity/DoctorPayment
        public ActionResult Index()
        {
            decimal total = 0;
            var listPayment = _doctorPaymentRepository.GetAllDoctorId(User.Identity.GetUserId());
            foreach (var item in listPayment)
            {
                total += item.TotalPaid;
            }

            ViewBag.TotalPaid = total;
            return View(listPayment);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorContribution doctorContribution = _contributionRepository.FindByDoctorPaymentId(id);
            if (doctorContribution == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderProduct = _orderRepository.Find(doctorContribution.OrderId).OrderProducts;
            return View(doctorContribution);
        }
    }
}