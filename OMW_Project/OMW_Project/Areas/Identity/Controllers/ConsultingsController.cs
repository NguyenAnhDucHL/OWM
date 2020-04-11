using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OMW_Project.Models;
using OMW_Project.Repositories;

namespace OMW_Project.Areas.Identity.Controllers
{
    public class ConsultingsController : Controller
    {

        private IConsultingRepository _consultingRepository;
        private IUserRepository _userRepository;
        public ConsultingsController()
        {
            _consultingRepository = new ConsultingRepository();
            _userRepository = new UserRepository();
        }

        // GET: Identity/Consultings
        public ActionResult Index()
        {
            var consultings = _consultingRepository.GetAllAvalable();
            return View(consultings);
        }
        public ActionResult MetAndMeeting()
        {
            var consultings = _consultingRepository.GetAllMet();
            return View(consultings);
        }
        public ActionResult NotMetYet()
        {
            var consultings = _consultingRepository.GetAllNotMet();
            return View(consultings);
        }
        // GET: Identity/Consultings/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulting consulting = _consultingRepository.Find(id);
            if (consulting == null)
            {
                return HttpNotFound();
            }
            return View(consulting);
        }

        // GET: Identity/Consultings/Create
        public ActionResult Create()
        {
            ViewBag.DoctorId = new SelectList(_userRepository.GetAllDoctor(), "Id", "FullName");
            return View();
        }

        // POST: Identity/Consultings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Consulting consulting)
        {
            if (consulting.StartConsulting != null)
            {
                if (consulting.StartConsulting.Date <= DateTime.Now.Date)
                {
                    ModelState.AddModelError("", "Lịch phải sau ngày hiện tại");
                }
                else
                {
                    var result = VerifyTime(consulting);
                    if (result != null)
                    {
                        ModelState.AddModelError("", "Thời gian không hợp lệ: " + result);
                    }
                }
            }


            if (ModelState.IsValid)
            {
                consulting.WebManagerId = User.Identity.GetUserId();
                _consultingRepository.Add(consulting);
                return RedirectToAction("Index");
            }

            ViewBag.DoctorId = new SelectList(_userRepository.GetAllDoctor(), "Id", "FullName", consulting.DoctorId);

            return View(consulting);
        }

        // GET: Identity/Consultings/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulting consulting = _consultingRepository.Find(id);
            if (consulting == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(_userRepository.GetAllDoctor(), "Id", "FullName", consulting.DoctorId);

            return View(consulting);
        }

        // POST: Identity/Consultings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Consulting consulting)
        {
            if (consulting.StartConsulting != null)
            {
                if (consulting.StartConsulting.Date <= DateTime.Now.Date)
                {
                    ModelState.AddModelError("", "Lịch phải sau ngày hiện tại");
                }
                else
                {
                    var result = VerifyTime(consulting);
                    if (result != null)
                    {
                        ModelState.AddModelError("", "Thời gian không hợp lệ: " + result);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                _consultingRepository.Update(consulting);
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(_userRepository.GetAllDoctor(), "Id", "FullName", consulting.DoctorId);
            return View(consulting);
        }

        // GET: Identity/Consultings/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulting consulting = _consultingRepository.Find(id);
            if (consulting == null)
            {
                return HttpNotFound();
            }
            return View(consulting);
        }

        // POST: Identity/Consultings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            _consultingRepository.Remove(id);
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult AddConsultingByDoc(Consulting consulting)
        {
            if (consulting.StartConsulting.Date <= DateTime.Now.Date)
            {
                return Json(new { IsSuccess = false, erroMsg = "Lịch phải bắt đầu từ ngày mai !!!" });
            }
            var result = VerifyTime(consulting);
            if (result == null)
            {
                _consultingRepository.Add(consulting);
                return Json(new { IsSuccess = true });
            }
            else
            {
                return Json(new { IsSuccess = false, erroMsg = "Thời gian trùng lặp: "+result });
            }
        }

        public string VerifyTime(Consulting consulting)
        {
            var nextHour = consulting.StartConsulting.AddHours(1);
            var preHour = consulting.StartConsulting.AddHours(-1);
            var notAllow = _consultingRepository.GetAll().Where(c => ((c.StartConsulting > preHour && c.StartConsulting <= consulting.StartConsulting && c.ConsultingId != consulting.ConsultingId)
                                                                  || (c.StartConsulting > consulting.StartConsulting && c.StartConsulting < nextHour && c.ConsultingId != consulting.ConsultingId))
                                                                  && c.DoctorId.Equals(consulting.DoctorId))
                                                                  .ToList();
            if (notAllow.Count > 0)
            {
                var result = "";
                foreach (var item in notAllow)
                {
                    result += item.StartConsulting.ToString("dd/MM/yyyy HH:mm");
                }
                return result;
            }
            return null;
        }


    }
}
