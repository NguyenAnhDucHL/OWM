using OMW_Project.Areas.Identity.ViewModels;
using OMW_Project.Models;
using OMW_Project.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OMW_Project.Repositories;

namespace OMW_Project.Areas.Identity.Controllers
{
    [Authorize(Roles = "Admin,WebManager")]
    public class DoctorProfileController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        private IDoctorProfileRepository _doctorProfileRepository;
        public DoctorProfileController()
        {
            _doctorProfileRepository = new DoctorProfileRepository();
        }

        public DoctorProfileController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            _doctorProfileRepository = new DoctorProfileRepository();
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Users/
        [HttpGet]
        public ActionResult Index()
        {
            return View(_doctorProfileRepository.GetAll());
        }

        //
        // GET: /Users/Details/5
        [HttpGet]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _doctorProfileRepository.Find(id);

            return View(user);
        }
        //
        // GET: /Users/Edit/1
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doc = _doctorProfileRepository.Find(id);
            if (doc == null)
            {
                return HttpNotFound();
            }
            return View(new EditUserViewModel()
            {
                Id = doc.UserId,
                Email = doc.User.Email,
                FullName = doc.User.FullName,
                Address = doc.User.Address,
                PhoneNumber = doc.User.PhoneNumber,
                Image = doc.User.Image,
                CMT = doc.CMT,
                DateRange = doc.DateRange,
                Experience = doc.Experience,
                Workplace = doc.Workplace,
                Sex = doc.User.Sex

            });
        }


        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel editUser, HttpPostedFileBase myfile)
        {

            if (myfile != null && myfile.ContentLength > 0)
            {
                string imgName = Path.GetFileName(myfile.FileName);
                string imgExt = Path.GetExtension(imgName);
                if (imgExt.Equals(".jpg") || imgExt.Equals(".jpeg") || imgExt.Equals(".png"))
                {
                    string imgPath = Path.Combine(Server.MapPath("~/Assets/img"), imgName);
                    myfile.SaveAs(imgPath);
                    editUser.Image = "/Assets/img/" + imgName;
                }
                else
                {
                    ModelState.AddModelError("", "Wrong file type");
                }
            }
            var user = await UserManager.FindByIdAsync(editUser.Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userRoles = await UserManager.GetRolesAsync(user.Id);
            if (ModelState.IsValid)
            {

                user.UserName = editUser.Email;
                user.Email = editUser.Email;
                user.Image = editUser.Image;
                user.FullName = editUser.FullName;
                user.Address = editUser.Address;
                user.PhoneNumber = editUser.PhoneNumber;
                user.Sex = editUser.Sex;

                var result = await UserManager.UpdateAsync(user);


                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                var doc = _doctorProfileRepository.Find(editUser.Id);
                doc.Experience = editUser.Experience;
                doc.Workplace = editUser.Workplace;
                doc.CMT = editUser.CMT;
                doc.DateRange = editUser.DateRange;
                _doctorProfileRepository.Update(doc);
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Something failed.");

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Assets/images/" + file.FileName));
            return file.FileName;
        }
    }
}
