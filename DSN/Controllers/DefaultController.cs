using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using DSN.Models;

namespace DSN.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Login()
        {
            return View();
        }

        //public void SetUserIdSession(LoginModel userCredentials)
        //{
        //    Session["Id"] = userCredentials.Id;
        //}

        //public void GetUserIdSession()
        //{

        //}

        [HttpPost]
        public ActionResult Login(LoginModel userCredentials)
        {
            Session[Constants.UserId] = userCredentials.Id;
            return RedirectToAction("Index", "Home");
        }
    }
}