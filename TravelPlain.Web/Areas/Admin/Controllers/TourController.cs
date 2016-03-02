using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelPlain.Business.DTO;
using TravelPlain.Business.Infrastructure;
using TravelPlain.Business.Interfaces;

namespace TravelPlain.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Manager")]
    public class TourController : Controller
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult Index(Web.ViewModels.Tour.FilterViewModel filter = null)
        {
            IEnumerable<TourDTO> tourDtos;
            if (ModelState.IsValid)
            {
                tourDtos = _tourService.Get(Mapper.Map<TourFilterDTO>(filter));
            }
            else
            {
                tourDtos = _tourService.Get();
            }

            var model = tourDtos
                .Select(o => Mapper.Map<Web.ViewModels.Tour.IndexViewModel>(o))
                .OrderByDescending(o => o.IsHot);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ViewModels.Tour.CreateViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModels.Tour.CreateViewModel model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string imageName = null;
                if (upload != null)
                {
                    if (upload.ContentLength > 0 &&
                                         upload.ContentType == "image/jpeg")
                    {
                        imageName = Guid.NewGuid().ToString() + ".jpg";
                        upload.SaveAs(Server.MapPath("~/Content/Images/") + imageName);
                    }
                    else
                    {
                        ModelState.AddModelError("Upload", "The file is not a valid JPEG image.");
                        return View(model);
                    }
                }
                var tourDto = Mapper.Map<TourDTO>(model);
                tourDto.ImageName = imageName;

                try
                {
                    _tourService.Create(tourDto);
                    _tourService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property.ToString(), ex.Message);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var tour = _tourService.GetById(id.Value);
                if (tour != null)
                {
                    return View(Mapper.Map<ViewModels.Tour.EditViewModel>(tour));
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModels.Tour.EditViewModel model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string imageName = null;
                    if (upload != null)
                    {
                        if (upload.ContentLength > 0 &&
                                             upload.ContentType == "image/jpeg")
                        {
                            imageName = Guid.NewGuid().ToString() + ".jpg";
                            upload.SaveAs(Server.MapPath("~/Content/Images/") + imageName);
                        }
                        else
                        {
                            ModelState.AddModelError("Upload", "The file is not a valid JPEG image.");
                            return View(model);
                        }
                    }
                    var tourDto = Mapper.Map<TourDTO>(model);
                    tourDto.ImageName = imageName;

                    _tourService.Update(tourDto);
                    _tourService.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ToggleHot(int? id)
        {
            if (id != null)
            {
                var tour =
                    Mapper.Map<Web.ViewModels.Tour.IndexViewModel>(_tourService.GetById(id.Value));
                if (tour != null)
                {
                    _tourService.ToggleHot(id.Value);
                    _tourService.SaveChanges();
                    return RedirectToAction("Index");
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}