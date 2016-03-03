using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelPlain.Business.Interfaces;
using TravelPlain.Web.Areas.Admin.ViewModels.Log;

namespace TravelPlain.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        public ActionResult Index()
        {
            var model = _logService.GetAll()
                .Select(o => Mapper.Map<IndexViewModel>(o));
            return View(model.OrderByDescending(o => o.Time));
        }
    }
}