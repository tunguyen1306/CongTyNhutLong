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
            var queryMax = (from u in db.tbl_OrderTem
                            orderby u.id descending
                            select u).Take(1);
            int max =  queryMax.ToList().Count==0?1: queryMax.ToList()[0].id+1;
            d.code = String.Format("DDH_N{0}T{1}N{2}_{3}", +DateTime.Now.Year, DateTime.Now.Month.ToString("00"), DateTime.Now.Day.ToString("00"), max.ToString("000"));
            var list = from tt in db.tbl_Customers where tt.IDCustomers == id.Value select tt;
            d.Customer = list.ToList()[0];
            return View(d);
        }

        // POST: tbl_OrderTem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DonHangView donHang)
        {
            if (ModelState.IsValid)
            {
              
                donHang.status = 0;
                donHang.BaoGiaTemView.status = 0;
                tbl_OrderTem temValue = new tbl_OrderTem { customer_id=donHang.customer_id,code=donHang.code,date_begin=donHang.date_begin,date_end=donHang.date_end,status=donHang.status,id=donHang.id };
                temValue=db.tbl_OrderTem.Add(temValue);
                db.SaveChanges();
                donHang.id = temValue.id;
                tbl_OrderTem_BaoGia tbl_OrderTem_BaoGia = new tbl_OrderTem_BaoGia { date_begin = donHang.BaoGiaTemView.date_begin, date_end = donHang.BaoGiaTemView.date_end, id = donHang.BaoGiaTemView.id, order_id = donHang.id, status = donHang.BaoGiaTemView.status, offset = donHang.BaoGiaTemView.offset, total_money = donHang.BaoGiaTemView.total_money };
                tbl_OrderTem_BaoGia = db.tbl_OrderTem_BaoGia.Add(tbl_OrderTem_BaoGia);
                db.SaveChanges();
                donHang.BaoGiaTemView.id = tbl_OrderTem_BaoGia.id;
                foreach (var item in donHang.BaoGiaTemView.BaoGiaTemDetailViews)
                {
                    item.StatusProducts = -1;
                    item.CreatedDateProducts = DateTime.Now;
                    item.CreatedDateProducts = DateTime.Now;
                    var queryMax = (from u in db.tbl_Products
                                    orderby u.ID_Products descending
                                    select u).Take(1);
                    int maxSP = queryMax.ToList().Count == 0 ? 1 : queryMax.ToList()[0].ID_Products + 1; 
                    String masp = String.Format("SP{0}", maxSP.ToString("000000"));
                    item.CodeProducts = masp;
                    tbl_Products itemP = new tbl_Products { CodeProducts = "",
                        CreatedDateProducts = item.CreatedDateProducts,
                        CreateUserProducts = item.CreateUserProducts,
                        DanKimProducts = item.DanKimProducts,
                        GiaProducts = item.GiaProducts,
                        ID_Products = item.ID_Products,
                        LoaigiayProducts = item.LoaigiayProducts,
                        ModifyDateProducts = item.ModifyDateProducts,
                        ModifyUserProducts = item.ModifyUserProducts,
                        NameProducts = item.NameProducts,
                        OffsetFlexoProducts = item.OffsetFlexoProducts,
                        QuyCachProducts = item.QuyCachProducts,
                        SolopProducts = item.SolopProducts,
                        StatusProducts = item.StatusProducts
                    };
                    itemP= db.tbl_Products.Add(itemP);
                    db.SaveChanges();
                    item.ID_Products = itemP.ID_Products;
                    tbl_OrderTem_BaoGia_Detail detail = new tbl_OrderTem_BaoGia_Detail { baogia_id = donHang.BaoGiaTemView.id, money = double.Parse(item.GiaProducts), soluong = item.SoLuong, sanpam_id = itemP.ID_Products };
                    db.tbl_OrderTem_BaoGia_Detail.Add(detail);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(donHang);
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
