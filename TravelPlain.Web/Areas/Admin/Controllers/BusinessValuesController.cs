using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelPlain.Business.DTO;
using TravelPlain.Business.Infrastructure;
using TravelPlain.Business.Interfaces;
using TravelPlain.Web.Areas.Admin.ViewModels.BusinessValues;

namespace TravelPlain.Web.Areas.Admin.Controllers
{
    public class BusinessValuesController : Controller
    {
        private readonly IBusinessValuesService _businessValuesService;

        public BusinessValuesController(IBusinessValuesService businessValuesService)
        {
            _businessValuesService = businessValuesService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _businessValuesService.GetAll()
                .Select(o => Mapper.Map<IndexViewModel>(o))
                .OrderByDescending(o => o.TimeSet);
            return View(model);
        }

        [HttpGet]
        public ActionResult Set()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Set(SetViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _businessValuesService.Create(Mapper.Map<BusinessValueDTO>(model));
                    _businessValuesService.SaveChanges();
                    return RedirectToAction("Index");
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