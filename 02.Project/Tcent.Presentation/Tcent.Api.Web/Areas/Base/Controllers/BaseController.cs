using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tcent.Api.Web.Areas.Base.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base/Base
        public ActionResult Index()
        {
            return View();
        }
    }
}