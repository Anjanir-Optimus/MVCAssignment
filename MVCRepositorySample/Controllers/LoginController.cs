using Assignment1.DAL;
using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Assignment1.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login objLogin)
        {
            if (ModelState.IsValid)
            {
                using (var objEntity = new Optimus_TradeEntities())
                {
                   
                    var result = objEntity.ClientDetails.Where(x => x.Email == objLogin.Email && x.Password == objLogin.Password).FirstOrDefault();
                    if (result != null)
                    {
                        FormsAuthentication.SetAuthCookie(objLogin.Email, objLogin.RememberMe);
                       
                      return  RedirectToAction("GetTransactionDetails", "OrderDetails");
                    }
                }
            }
            return View(objLogin);
        }

    }
}
