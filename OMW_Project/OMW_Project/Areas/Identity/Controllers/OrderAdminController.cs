using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OMW_Project.Models;
using OMW_Project.Repositories;

namespace OMW_Project.Areas.Identity.Controllers
{
    public class OrderAdminController : Controller
    {

        private IOrderRepository _orderRepository;
        public OrderAdminController()
        {
            _orderRepository = new OrderRepository();
        }
        // GET: Identity/OrderAdmin
        public ActionResult Index()
        {
            var listOrder = _orderRepository.GetAll();
            return View(listOrder);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderRepository.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}