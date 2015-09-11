using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class testController : Controller
    {
        //
        // GET: /test/

        public ActionResult Index(Product model)
        {
            return View();
        }

    }
}
