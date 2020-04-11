using OMW_Project.Areas.Identity.ViewModels;
using OMW_Project.Models;
using OMW_Project.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OMW_Project.Repositories;
using System.IO;
using System;
using Microsoft.AspNet.Identity;

namespace OMW_Project.Areas.Identity.Controllers
{
    [Authorize(Roles = "Admin,Doctor")]
    public class UsersAdminController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        private IDoctorProfileRepository _doctorProfileRepository;
        private IConsultingRepository _consultingRepository;
        public UsersAdminController()
        {
            _doctorProfileRepository = new DoctorProfileRepository();
            _consultingRepository = new ConsultingRepository();
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        //
        // GET: /Users/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            ViewBag.DoctorProfile = _doctorProfileRepository.Find(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles

            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, HttpPostedFileBase myfile, params string[] selectedRoles)
        {
            if (myfile != null && myfile.ContentLength > 0)
            {
                string imgName = Path.GetFileName(myfile.FileName);
                string imgExt = Path.GetExtension(imgName);
                if (imgExt.Equals(".jpg") || imgExt.Equals(".jpeg") || imgExt.Equals(".png"))
                {
                    string imgPath = Path.Combine(Server.MapPath("~/Assets/img"), imgName);
                    myfile.SaveAs(imgPath);
                    userViewModel.Image = "/Assets/img/" + imgName;
                }
                else
                {
                    ModelState.AddModelError("", "Sai loại tệp");
                }
            }
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email,
                    FullName = userViewModel.FullName,
                    Address = userViewModel.Address,
                    PhoneNumber = userViewModel.PhoneNumber,
                    Image = userViewModel.Image,
                    Sex=userViewModel.Sex
                };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                if (selectedRoles.Length > 0 && selectedRoles[0].Equals("Doctor") && adminresult.Succeeded)
                {
                    var doctor = new DoctorProfile
                    {
                        UserId = user.Id,
                        CMT = userViewModel.CMT,
                        DateRange = userViewModel.DateRange.Value,
                        Experience = userViewModel.Experience,
                        Workplace = userViewModel.Workplace
                    };
                    _doctorProfileRepository.Add(doctor);

                }
                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại.");
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");

            return View();
        }

        //
        // GET: /Users/Edit/1
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);
            if (userRoles.Contains("Doctor"))
            {
                var docProfile = _doctorProfileRepository.Find(user.Id);
                return View(new EditUserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                    {
                        Selected = userRoles.Contains(x.Name),
                        Text = x.Name,
                        Value = x.Name
                    }),
                    FullName = user.FullName,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    Image = user.Image,
                    CMT = docProfile.CMT,
                    DateRange = docProfile.DateRange,
                    Experience = docProfile.Experience,
                    Workplace = docProfile.Workplace,
                    Sex=user.Sex

                });
            }

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                }),
                FullName = user.FullName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Image = user.Image,
                Sex=user.Sex

            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel editUser, HttpPostedFileBase myfile, params string[] selectedRole)
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


                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                if(selectedRole.Contains("Doctor") && !userRoles.Contains("Doctor"))
                {
                    var doctor = new DoctorProfile
                    {
                        UserId = user.Id,
                        CMT = editUser.CMT,
                        DateRange = editUser.DateRange,
                        Experience = editUser.Experience,
                        Workplace = editUser.Workplace
                    };
                    _doctorProfileRepository.Add(doctor);
                }
                else if(!selectedRole.Contains("Doctor") && userRoles.Contains("Doctor"))
                {
                    _doctorProfileRepository.Remove(user.Id);
                }
                else if (selectedRole.Contains("Doctor") && userRoles.Contains("Doctor"))
                {
                    var doctor = new DoctorProfile
                    {
                        UserId = user.Id,
                        CMT = editUser.CMT,
                        DateRange = editUser.DateRange,
                        Experience = editUser.Experience,
                        Workplace = editUser.Workplace
                    };
                    _doctorProfileRepository.Update(doctor);
                }
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

        //
        // GET: /Users/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                _doctorProfileRepository.Remove(user.Id);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Assets/images/" + file.FileName));
            return file.FileName;
            
        }
        [Authorize]
        public ActionResult MyProfile()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if(User.IsInRole("Doctor"))
            {
                ViewBag.docProfile = _doctorProfileRepository.Find(user.Id);
                ViewBag.consulting = _consultingRepository.GetAvailableByDoctorId(user.Id);
            }
            return View(user);
        }
    }
}
