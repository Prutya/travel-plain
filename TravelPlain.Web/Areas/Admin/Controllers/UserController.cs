using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TravelPlain.Business.Interfaces;
using TravelPlain.Web.Areas.Admin.ViewModels.User;

namespace TravelPlain.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IProfileService _profileService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UserController(
            IProfileService profileService)
        {
            _profileService = profileService;
        }

        public UserController(
            IProfileService profileService,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager)
        {
            _profileService = profileService;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
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

        private IEnumerable<SelectListItem> GetRoles()
        {
            IEnumerable<IdentityRole> roles;
            using (var context = new Models.ApplicationDbContext())
            {
                roles = context.Roles.ToList();
            }

            var list = roles.Select(role => new SelectListItem
            {
                Value = role.Name
            });

            return new SelectList(list, "Value", "Value");
        }

        private IEnumerable<SelectListItem> GetUserRoles(Models.ApplicationUser user)
        {
            var list = UserManager.GetRoles(user.Id)
                .Select(o => new SelectListItem
                {
                    Value = o
                });

            return new SelectList(list, "Value", "Value");
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<ViewModels.User.IndexViewModel> model;
            IEnumerable<Models.ApplicationUser> users;

            var profilesDtos = _profileService.GetAll();

            using (var context = new Models.ApplicationDbContext())
            {
                users = context.Users.ToList();
            }

            model = (from u in users
                     join p in profilesDtos on u.Id equals p.Id into tempTbl
                     from up in tempTbl.DefaultIfEmpty()
                     select new IndexViewModel
                     {
                         Id = u.Id,
                         Email = u.Email,
                         PhoneNumber = u.PhoneNumber,
                         LockedOutTill = u.LockoutEndDateUtc ?? default(DateTime),
                         Roles = UserManager.GetRoles(u.Id),
                         FirstName = up != null ? up.FirstName : null,
                         LastName = up != null ? up.LastName : null
                     })
            .ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> SetLockout(string id)
        {
            if (id != null)
            {
                var user = await UserManager.FindByIdAsync(id);

                if (user != null)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEndDateUtc = DateTimeOffset.Now.UtcDateTime.AddYears(50);
                    UserManager.Update(user);
                    return RedirectToAction("Index");
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public async Task<ActionResult> RemoveLockout(string id)
        {
            if (id != null)
            {
                var user = await UserManager.FindByIdAsync(id);

                if (user != null)
                {
                    user.LockoutEnabled = false;
                    user.LockoutEndDateUtc = null;
                    UserManager.Update(user);
                    return RedirectToAction("Index");
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public async Task<ActionResult> AddToRole(string id)
        {
            if (id != null)
            {
                var user = await UserManager.FindByIdAsync(id);
                if (user != null)
                {
                    var model = new AddToRoleViewModel
                    {
                        UserId = user.Id,
                        UserEmail = user.Email,
                        Roles = GetRoles()
                    };

                    return View(model);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToRole(AddToRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    var result = UserManager.AddToRole(user.Id, model.Role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("addToRoleResult", "Error adding role");
                    model.Roles = GetRoles();
                    return View(model);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            model.Roles = GetRoles();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> RemoveFromRole(string id)
        {
            if (id != null)
            {
                var user = await UserManager.FindByIdAsync(id);
                if (user != null)
                {
                    var model = new AddToRoleViewModel
                    {
                        UserId = user.Id,
                        UserEmail = user.Email,
                        Roles = GetUserRoles(user)
                    };

                    return View(model);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveFromRole(AddToRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    var result = UserManager.RemoveFromRole(user.Id, model.Role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("removeFromRoleResult", "Error removing role");
                    model.Roles = GetUserRoles(user);
                    return View(model);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (model.UserId != null)
            {
                var user = await UserManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    model.Roles = GetUserRoles(user);
                }
                return View(model);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
    }
}