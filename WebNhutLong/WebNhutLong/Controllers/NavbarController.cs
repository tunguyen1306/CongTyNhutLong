using WebNhutLong.Domain;
using WebNhutLong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebNhutLong.Controllers
{
    public class NavbarController : Controller
    {
        // GET: Navbar
        public ActionResult Index()
        {
            var data = new Data();
            return PartialView("_Navbar", data.navbarItems().ToList());
        }
        public ActionResult Logout()
        {
            Session.Remove("username");
            Session.Clear();
            return RedirectToAction("Login","Login");
        }
    }
}