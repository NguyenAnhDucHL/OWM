using Microsoft.AspNet.Identity;
using OMW_Project.Models;
using OMW_Project.Repositories;
using System.Web.Mvc;

namespace OMW_Project.Controllers
{
    public class ConsultingController : Controller
    {
        private IConsultingRepository _consultingRepository;
        private IPostRepository _postRepository;
        private IUserRepository _userRepository;
        private IDoctorProfileRepository _doctorProfileRepository;
        public ConsultingController()
        {
            _consultingRepository = new ConsultingRepository();
            _userRepository = new UserRepository();
            _postRepository = new PostRepository();
            _doctorProfileRepository = new DoctorProfileRepository();
        }
        // GET: Consulting
        public ActionResult Index()
        {
            return View();
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
            var data = _doctorProfileRepository.Find(docId);
            ViewBag.consultings = _consultingRepository.GetAllForBook(docId);
            ViewBag.lstCatePost = _postRepository.GetPost_Category();
            return View(data);
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
    }
}