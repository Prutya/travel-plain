using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web.Mvc;
using TravelPlain.Business.DTO;
using TravelPlain.Business.Infrastructure;
using TravelPlain.Business.Interfaces;

namespace TravelPlain.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ITourService _tourService;

        public OrderController(IOrderService orderService, ITourService tourService)
        {
            _orderService = orderService;
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult Place(int? tourId)
        {
            if (tourId != null)
            {
                var tour = _tourService.GetById(tourId.Value);
                if(tour != null)
                {
                    var model = Mapper.Map<ViewModels.Order.PlaceViewModel>(tour);

                    try
                    {
                        model.Price = _orderService.CalculatePrice(tour.Id, User.Identity.GetUserId());
                        model.YouSave = tour.Price - model.Price;
                    }
                    catch (ValidationException ex)
                    {
                        ModelState.AddModelError(ex.Property, ex.Message);
                        //TODO: or return new HttpStatusCodeResult(HttpStatusCode.NotFound) ?
                    }

                    return View(model);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return  new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Place(ViewModels.Order.PlaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var order = Mapper.Map<OrderDTO>(model);
                    order.ProfileId = User.Identity.GetUserId();
                    _orderService.Place(order);
                    _orderService.SaveChanges();
                    return View("Success", model);
                }
                catch(ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Cancel(int? id)
        {
            if (id != null)
            {
                var order = _orderService.GetById(id.Value);
                if (order != null)
                {
                    if (order.ProfileId == User.Identity.GetUserId())
                    {
                        if (order.Status != Enums.OrderStatus.Cancelled)
                        {
                            var model = Mapper.Map<ViewModels.Order.IndexViewModel>(order);
                            return View(model);
                        }
                        return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(ViewModels.Order.IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _orderService.Cancel(model.Id, User.Identity.GetUserId());
                    _orderService.SaveChanges();
                    return RedirectToAction("Index", "Manage");
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