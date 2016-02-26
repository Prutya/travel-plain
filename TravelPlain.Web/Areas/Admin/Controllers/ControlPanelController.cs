using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelPlain.Web.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class ControlPanelController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}