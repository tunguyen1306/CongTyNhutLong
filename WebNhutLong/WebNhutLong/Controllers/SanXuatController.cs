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
          
            var qr = (from data in db.tbl_OrderTem
                join cus in db.tbl_Customers on data.customer_id equals cus.IDCustomers
                where data.status == 1
                select new DonHangView
                {
                    id = data.id,
                    customer_id = cus.IDCustomers,
                    Customer = cus,
                    code=data.code,
                    date_begin = data.date_begin,
                    date_end = data.date_end,
                    status = data.status
                        

                    
                });
           
            return View(qr.ToList());
        }
    }
}