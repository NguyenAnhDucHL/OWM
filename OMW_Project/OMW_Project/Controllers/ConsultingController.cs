using System.Net;
using Microsoft.AspNet.Identity;
using OMW_Project.Models;
using OMW_Project.Repositories;
using System.Web.Mvc;

namespace OMW_Project.Controllers
{
    public class ConsultingController : Controller
    {
        private IConsultingRepository _consultingRepository;
        private IConsultResultRepository _consultResultRepository;
        private IPostRepository _postRepository;
        private IUserRepository _userRepository;
        private IDoctorProfileRepository _doctorProfileRepository;
        public ConsultingController()
        {
            _consultingRepository = new ConsultingRepository();
            _userRepository = new UserRepository();
            _postRepository = new PostRepository();
            _doctorProfileRepository = new DoctorProfileRepository();
            _consultResultRepository = new ConsultResultRepository();
        }
        // GET: Consulting
        public ActionResult Index()
        {
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            var consult = _consultingRepository.CheckUserId(User.Identity.GetUserId());
            return View(consult);
        }
        [Authorize]
        public ActionResult Book()
        {
            var doctors = _userRepository.GetAllDoctor();
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            return View(doctors);
        }
        [Authorize]
        public ActionResult DetailDoctor(string docId)
        {
            var consult = _consultingRepository.CheckUserId(User.Identity.GetUserId());
            if (consult == null)
            {
                var data = _doctorProfileRepository.Find(docId);
                ViewBag.consultings = _consultingRepository.GetAllForBook(docId);
                ViewBag.lstCatePost = _postRepository.GetPost_Category();
                return View(data);
            }
            else
            {
                return RedirectToAction("Index","Consulting");
            }
            
        }
        [HttpPost]
        [Authorize]
        public ActionResult Book(Consulting consulting)
        {
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            var userID = User.Identity.GetUserId();
            _consultingRepository.SaveBook(consulting, userID);
            return RedirectToAction("Index","Home");
        }
        [Authorize]
        public ActionResult History()
        {
            var userId = User.Identity.GetUserId();
            var data = _consultingRepository.GetHistoryByPatient(userId);
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            return View(data);
        }
        [Authorize]
        public ActionResult ViewResult(string consultingId)
        {
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            var result = _consultResultRepository.FindByConsultingId(consultingId);
            return View(result);
        }
        [Authorize]
        public ActionResult ConsultingPresent()
        {
            var userId = User.Identity.GetUserId();
            var data = _consultingRepository.GetHistoryByPatientPresent(userId);
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            return View(data);
        }
        [Authorize]
        public ActionResult EditConsulting(string id)
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
            
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            return View(consulting);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditConsulting(Consulting consulting)
        {
            if (ModelState.IsValid)
            {
                _consultingRepository.Update(consulting);
                return RedirectToAction("ConsultingPresent");
            }
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            return View(consulting);
        }
    }
}