using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TravelPlain.Business.DTO;
using TravelPlain.Business.Interfaces;

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
    }
}