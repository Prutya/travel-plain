using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelPlain.Business.Infrastructure;
using TravelPlain.Business.Interfaces;

namespace TravelPlain.Web.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProfileService _profileService;
        private ApplicationUserManager _userManager;

        public OrderController(IOrderService orderService, IProfileService profileService)
        {
            _orderService = orderService;
            _profileService = profileService;
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

        [HttpGet]
        public ActionResult Index()
        {
            var model = _orderService.GetAll()
                .Select(o => Mapper.Map<ViewModels.Order.IndexViewModel>(o))
                .OrderBy(o => o.Status).ThenBy(o => o.Time).ToList();

            foreach (var order in model)
            {
                var info = _profileService.Get(order.UserId);
                order.UserName = info.FirstName + " " + info.LastName;
                order.UserEmail = UserManager.GetEmail(order.UserId);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult ChangeStatus(int? id)
        {
            if (id != null)
            {
                var model = Mapper.Map<ViewModels.Order.IndexViewModel>
                    (_orderService.GetById(id.Value));
                if (model != null)
                {
                    return View(model);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(ViewModels.Order.IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _orderService.ChangeOrderStatus(model.Id, model.Status);
                    _orderService.SaveChanges();
                    return RedirectToAction("Index", "Order");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            return View(model);
        }
    }
}