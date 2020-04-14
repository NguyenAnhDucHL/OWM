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
    [Authorize(Roles = "Admin,WebManager")]
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
        public ActionResult Processed()
        {
            var listOrder = _orderRepository.GetAllProcessed();
            return View(listOrder);
        }
        public ActionResult NoProcessed()
        {
            var listOrder = _orderRepository.GetAllNoProcessed();
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
        public ActionResult Edit(string id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Update(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }
        // GET: Identity/Posts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order post = _orderRepository.Find(id);
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
            _orderRepository.Remove(id);
            return RedirectToAction("Index");
        }
        public ActionResult GetAllThisMonth()
        {
            var listOrder = _orderRepository.GetAllThisMonth();
            double total = 0;
            foreach (var item in listOrder)
            {
                
            }
            return View(listOrder);
        }
    }
}