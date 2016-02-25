using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelPlain.Business.DTO;
using TravelPlain.Business.Infrastructure;
using TravelPlain.Business.Interfaces;
using TravelPlain.Enums;

namespace TravelPlain.Web.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult Index(ViewModels.Tour.FilterViewModel filter = null)
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
                .Select(o => Mapper.Map<ViewModels.Tour.IndexViewModel>(o))
                .OrderByDescending(o => o.IsHot);
            return View(model);
        }

        /*
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
        */
    }
}