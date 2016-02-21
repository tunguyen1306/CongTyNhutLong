using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhutLong.Models;

namespace WebNhutLong.Controllers
{
    public class SanXuatController : Controller
    {
        private WebNhutLongEntities db = new WebNhutLongEntities();
        // GET: SanXuat
        public ActionResult Index()
        {
            var qr = from data in db.tbl_OrderTem
                where data.status == 1
                select data;
            return View(qr.ToList());
        }
    }
}