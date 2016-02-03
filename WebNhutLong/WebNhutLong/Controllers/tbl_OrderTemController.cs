using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNhutLong.Models;

namespace WebNhutLong.Controllers
{
    public class tbl_OrderTemController : Controller
    {
        private WebNhutLongEntities db = new WebNhutLongEntities();

        // GET: tbl_OrderTem
        public ActionResult Index()
        {
            return View(db.tbl_OrderTem.ToList());
        }

        // GET: tbl_OrderTem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_OrderTem tbl_OrderTem = db.tbl_OrderTem.Find(id);
            if (tbl_OrderTem == null)
            {
                return HttpNotFound();
            }
            return View(tbl_OrderTem);
        }

        // GET: tbl_OrderTem/Create
        public ActionResult Create(int? id)
        {
            DonHangView d = new DonHangView();
            d.customer_id = id;
            d.code = "DDH" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            var list = from tt in db.tbl_Customers where tt.IDCustomers == id.Value select tt;
            d.Customer = list.ToList()[0];
            return View(d);
        }

        // POST: tbl_OrderTem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DonHangView tbl_OrderTem)
        {
            if (ModelState.IsValid)
            {
                //db.tbl_OrderTem.Add(tbl_OrderTem);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_OrderTem);
        }

        // GET: tbl_OrderTem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_OrderTem tbl_OrderTem = db.tbl_OrderTem.Find(id);
            if (tbl_OrderTem == null)
            {
                return HttpNotFound();
            }
            return View(tbl_OrderTem);
        }

        // POST: tbl_OrderTem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,code,date_begin,date_end,customer_id,status")] tbl_OrderTem tbl_OrderTem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_OrderTem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_OrderTem);
        }

        // GET: tbl_OrderTem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_OrderTem tbl_OrderTem = db.tbl_OrderTem.Find(id);
            if (tbl_OrderTem == null)
            {
                return HttpNotFound();
            }
            return View(tbl_OrderTem);
        }

        // POST: tbl_OrderTem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_OrderTem tbl_OrderTem = db.tbl_OrderTem.Find(id);
            db.tbl_OrderTem.Remove(tbl_OrderTem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
