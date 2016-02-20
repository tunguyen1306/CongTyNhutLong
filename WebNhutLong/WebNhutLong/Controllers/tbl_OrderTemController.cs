﻿using System;
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
            int max = queryMax.ToList().Count == 0 ? 1 : queryMax.ToList()[0].id + 1;
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
           
                donHang.status = 0;
                donHang.BaoGiaTemView.status = 0;
                tbl_OrderTem temValue = new tbl_OrderTem { customer_id = donHang.customer_id, code = donHang.code, date_begin = donHang.date_begin, date_end = donHang.date_end, status = donHang.status, id = donHang.id };
                temValue = db.tbl_OrderTem.Add(temValue);
                db.SaveChanges();
                donHang.id = temValue.id;
                donHang.BaoGiaTemView.date_begin = DateTime.Now;
                tbl_OrderTem_BaoGia tbl_OrderTem_BaoGia = new tbl_OrderTem_BaoGia { date_begin = donHang.BaoGiaTemView.date_begin, date_end = donHang.BaoGiaTemView.date_end, id = donHang.BaoGiaTemView.id, order_id = donHang.id, status = donHang.BaoGiaTemView.status, offset = donHang.BaoGiaTemView.offset, total_money = donHang.BaoGiaTemView.total_money };
                tbl_OrderTem_BaoGia = db.tbl_OrderTem_BaoGia.Add(tbl_OrderTem_BaoGia);
                db.SaveChanges();
                donHang.BaoGiaTemView.id = tbl_OrderTem_BaoGia.id;
                foreach (var item in donHang.BaoGiaTemView.BaoGiaTemDetailViews)
                {
                    item.StatusProducts = -1;
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
                    itemP = db.tbl_Products.Add(itemP);
                    db.SaveChanges();
                    item.ID_Products = itemP.ID_Products;
                    tbl_OrderTem_BaoGia_Detail detail = new tbl_OrderTem_BaoGia_Detail { baogia_id = donHang.BaoGiaTemView.id, money = double.Parse(item.GiaProducts), soluong = item.SoLuong, sanpam_id = itemP.ID_Products };
                    db.tbl_OrderTem_BaoGia_Detail.Add(detail);
                    db.SaveChanges();
                }
            return RedirectToAction("Edit", new
            {
                id = donHang.id 
                    });
           
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
          
            DonHangView d = new DonHangView();
            d.customer_id = tbl_OrderTem.customer_id;
            d.code = tbl_OrderTem.code;
            d.date_begin = tbl_OrderTem.date_begin;
            d.date_end = tbl_OrderTem.date_end;
            d.id = tbl_OrderTem.id;
            d.status = tbl_OrderTem.status;

            var queryBaoGia = from u in db.tbl_OrderTem_BaoGia where u.order_id.Value.Equals(tbl_OrderTem.id) orderby u.id descending select u;
            List<tbl_OrderTem_BaoGia> lisBG = queryBaoGia.ToList<tbl_OrderTem_BaoGia>();
            List<BaoGiaTemView> lisBGTem = new List<BaoGiaTemView>();

            if (lisBG.Count>1)
            {
                for (int i = 1; i < lisBG.Count; i++)
                {
                    var item = lisBG[i];
                    BaoGiaTemView temBG = new BaoGiaTemView { date_begin = item.date_begin, date_end = item.date_end, id = item.id, offset = item.offset, order_id = item.order_id, status = item.status, total_money = item.total_money };
                    var queryGiaoGiaCT = from u in db.tbl_OrderTem_BaoGia_Detail
                                         join y in db.tbl_Products on u.sanpam_id equals y.ID_Products
                                         where u.baogia_id.Value.Equals(temBG.id)
                                         select new BaoGiaTemDetailView { ID_Products = u.sanpam_id.Value, CodeProducts = y.CodeProducts, CreatedDateProducts = y.CreatedDateProducts, CreateUserProducts = y.CreateUserProducts, DanKimProducts = y.DanKimProducts, GiaProducts = u.money.Value.ToString(), LoaigiayProducts = y.LoaigiayProducts, ModifyDateProducts = y.ModifyDateProducts, ModifyUserProducts = y.ModifyUserProducts, NameProducts = y.NameProducts, OffsetFlexoProducts = y.OffsetFlexoProducts, QuyCachProducts = y.QuyCachProducts, SolopProducts = y.SolopProducts, SoLuong = u.soluong.Value, StatusProducts = y.StatusProducts };
                    temBG.BaoGiaTemDetailViews = queryGiaoGiaCT.ToList<BaoGiaTemDetailView>();
                    lisBGTem.Add(temBG);
                }
            }         
           
            d.BaoGiaTemViews = lisBGTem;
            queryBaoGia = from u in db.tbl_OrderTem_BaoGia where u.order_id.Value.Equals(tbl_OrderTem.id)  orderby u.id descending select u;
            lisBG = queryBaoGia.ToList<tbl_OrderTem_BaoGia>();
            foreach (var item in lisBG)
            {
                BaoGiaTemView temBG = new BaoGiaTemView { date_begin = item.date_begin, date_end = item.date_end, id = item.id, offset = item.offset, order_id = item.order_id, status = item.status, total_money = item.total_money };
                var queryGiaoGiaCT = from u in db.tbl_OrderTem_BaoGia_Detail
                                     join y in db.tbl_Products on u.sanpam_id equals y.ID_Products where u.baogia_id.Value.Equals(temBG.id)
                                     select new BaoGiaTemDetailView { ID_Products = u.sanpam_id.Value, CodeProducts = y.CodeProducts, CreatedDateProducts = y.CreatedDateProducts, CreateUserProducts = y.CreateUserProducts, DanKimProducts = y.DanKimProducts, GiaProducts = u.money.Value.ToString(), LoaigiayProducts = y.LoaigiayProducts, ModifyDateProducts = y.ModifyDateProducts, ModifyUserProducts = y.ModifyUserProducts, NameProducts = y.NameProducts, OffsetFlexoProducts = y.OffsetFlexoProducts, QuyCachProducts = y.QuyCachProducts, SolopProducts = y.SolopProducts, SoLuong = u.soluong.Value, StatusProducts = y.StatusProducts };
                temBG.BaoGiaTemDetailViews = queryGiaoGiaCT.ToList<BaoGiaTemDetailView>();
                d.BaoGiaTemView = temBG;
                break;
            }
                var list = from tt in db.tbl_Customers where tt.IDCustomers == d.customer_id select tt;
            d.Customer = list.ToList()[0];
            return View(d);
        }

        // POST: tbl_OrderTem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DonHangView donHang)
        {
            int? id = donHang.id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_OrderTem tbl_OrderTem = db.tbl_OrderTem.Find(id);
            if (tbl_OrderTem == null)
            {
                return HttpNotFound();
            }
            if (donHang.action==2)
            {
                donHang.action = 0;
                donHang.BaoGiaTemView.status = 0;
                donHang.BaoGiaTemView.date_begin = DateTime.Now;
                tbl_OrderTem_BaoGia tbl_OrderTem_BaoGia = new tbl_OrderTem_BaoGia { date_begin = donHang.BaoGiaTemView.date_begin, date_end = donHang.BaoGiaTemView.date_end, id = donHang.BaoGiaTemView.id, order_id = donHang.id, status = donHang.BaoGiaTemView.status, offset = donHang.BaoGiaTemView.offset, total_money = donHang.BaoGiaTemView.total_money };
                tbl_OrderTem_BaoGia = db.tbl_OrderTem_BaoGia.Add(tbl_OrderTem_BaoGia);
                db.SaveChanges();
                donHang.BaoGiaTemView.id = tbl_OrderTem_BaoGia.id;
                foreach (var item in donHang.BaoGiaTemView.BaoGiaTemDetailViews)
                {
                    item.StatusProducts = -1;
                    item.CreatedDateProducts = DateTime.Now;

                    var queryMax = (from u in db.tbl_Products
                                    orderby u.ID_Products descending
                                    select u).Take(1);
                    int maxSP = queryMax.ToList().Count == 0 ? 1 : queryMax.ToList()[0].ID_Products + 1;
                    String masp = String.Format("SP{0}", maxSP.ToString("000000"));
                    item.CodeProducts = masp;
                    tbl_Products itemP = new tbl_Products
                    {
                        CodeProducts = "",
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
                    itemP = db.tbl_Products.Add(itemP);
                    db.SaveChanges();
                    item.ID_Products = itemP.ID_Products;
                    tbl_OrderTem_BaoGia_Detail detail = new tbl_OrderTem_BaoGia_Detail { baogia_id = donHang.BaoGiaTemView.id, money = double.Parse(item.GiaProducts), soluong = item.SoLuong, sanpam_id = itemP.ID_Products };
                    db.tbl_OrderTem_BaoGia_Detail.Add(detail);
                    db.SaveChanges();
                }
             
            }
            if (donHang.action == 3)
            {
                donHang.action = 0;
                tbl_OrderTem_BaoGia baogia = db.tbl_OrderTem_BaoGia.Find(donHang.BaoGiaTemView.id);
                baogia.status =donHang.BaoGiaTemView.status.Value;
                baogia.date_end = DateTime.Now;
                baogia.note = donHang.BaoGiaTemView.note;
                db.Entry(baogia).State = EntityState.Modified;
                db.SaveChanges();
               
            }
            if (donHang.action == 4)
            {
                donHang.action = 0;
                tbl_OrderTem order = db.tbl_OrderTem.Find(donHang.id);
                order.date_begin_plan = donHang.date_begin_plan.Value;
                order.date_end_plan = donHang.date_end_plan.Value;
                order.status = donHang.status.Value;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();


                tbl_OrderTem_BaoGia baogia = db.tbl_OrderTem_BaoGia.Find(donHang.BaoGiaTemView.id);
                baogia.flow = donHang.BaoGiaTemView.flow;             
                db.Entry(baogia).State = EntityState.Modified;
                db.SaveChanges();
            }
            DonHangView d = new DonHangView();
            d.id = donHang.id;
            d.action = donHang.action;
            d.customer_id = tbl_OrderTem.customer_id;
            d.code = tbl_OrderTem.code;
            d.date_begin = tbl_OrderTem.date_begin;
            d.date_end = tbl_OrderTem.date_end;
            d.id = tbl_OrderTem.id;
            d.status = tbl_OrderTem.status;

            var queryBaoGia = from u in db.tbl_OrderTem_BaoGia where u.order_id.Value.Equals(tbl_OrderTem.id) orderby u.id descending select u;
            List<tbl_OrderTem_BaoGia> lisBG = queryBaoGia.ToList<tbl_OrderTem_BaoGia>();
            List<BaoGiaTemView> lisBGTem = new List<BaoGiaTemView>();

            if (lisBG.Count > 1)
            {
                for (int i = 1; i < lisBG.Count; i++)
                {
                    var item = lisBG[i];
                    BaoGiaTemView temBG = new BaoGiaTemView { date_begin = item.date_begin, date_end = item.date_end, id = item.id, offset = item.offset, order_id = item.order_id, status = item.status, total_money = item.total_money };
                    var queryGiaoGiaCT = from u in db.tbl_OrderTem_BaoGia_Detail
                                         join y in db.tbl_Products on u.sanpam_id equals y.ID_Products
                                         where u.baogia_id.Value.Equals(temBG.id)
                                         select new BaoGiaTemDetailView { ID_Products = u.sanpam_id.Value, CodeProducts = y.CodeProducts, CreatedDateProducts = y.CreatedDateProducts, CreateUserProducts = y.CreateUserProducts, DanKimProducts = y.DanKimProducts, GiaProducts = u.money.Value.ToString(), LoaigiayProducts = y.LoaigiayProducts, ModifyDateProducts = y.ModifyDateProducts, ModifyUserProducts = y.ModifyUserProducts, NameProducts = y.NameProducts, OffsetFlexoProducts = y.OffsetFlexoProducts, QuyCachProducts = y.QuyCachProducts, SolopProducts = y.SolopProducts, SoLuong = u.soluong.Value, StatusProducts = y.StatusProducts };
                    temBG.BaoGiaTemDetailViews = queryGiaoGiaCT.ToList<BaoGiaTemDetailView>();
                    lisBGTem.Add(temBG);
                }
            }

            d.BaoGiaTemViews = lisBGTem;
            queryBaoGia = from u in db.tbl_OrderTem_BaoGia where u.order_id.Value.Equals(tbl_OrderTem.id) orderby u.id descending select u;
            lisBG = queryBaoGia.ToList<tbl_OrderTem_BaoGia>();
            foreach (var item in lisBG)
            {
                BaoGiaTemView temBG = new BaoGiaTemView { date_begin = item.date_begin, date_end = item.date_end, id = item.id, offset = item.offset, order_id = item.order_id, status = item.status, total_money = item.total_money };
                var queryGiaoGiaCT = from u in db.tbl_OrderTem_BaoGia_Detail
                                     join y in db.tbl_Products on u.sanpam_id equals y.ID_Products
                                     where u.baogia_id.Value.Equals(temBG.id)
                                     select new BaoGiaTemDetailView { ID_Products = u.sanpam_id.Value, CodeProducts = y.CodeProducts, CreatedDateProducts = y.CreatedDateProducts, CreateUserProducts = y.CreateUserProducts, DanKimProducts = y.DanKimProducts, GiaProducts = u.money.Value.ToString(), LoaigiayProducts = y.LoaigiayProducts, ModifyDateProducts = y.ModifyDateProducts, ModifyUserProducts = y.ModifyUserProducts, NameProducts = y.NameProducts, OffsetFlexoProducts = y.OffsetFlexoProducts, QuyCachProducts = y.QuyCachProducts, SolopProducts = y.SolopProducts, SoLuong = u.soluong.Value, StatusProducts = y.StatusProducts };
                temBG.BaoGiaTemDetailViews = queryGiaoGiaCT.ToList<BaoGiaTemDetailView>();
                d.BaoGiaTemView = temBG;
                break;
            }
            var list = from tt in db.tbl_Customers where tt.IDCustomers == d.customer_id select tt;
            d.Customer = list.ToList()[0];
            return View(d);
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
        public ActionResult PrintOrder()
        {
            return View();
        }
         [HttpPost]
        public ActionResult PrintOrder(int id)
        {
            var qr = (from data in db.tbl_OrderTem
                     join cus in db.tbl_Customers on data.id equals cus.IDCustomers
                     where data.id==id
                     select new { data ,cus}).ToList().Select(x=>new DonHangView
                     {
                         customer_id = x.cus.IDCustomers
                     });
           
            return View(qr.ToList());
        }
       
    }
}
