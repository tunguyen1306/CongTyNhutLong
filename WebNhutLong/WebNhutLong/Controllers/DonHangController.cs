using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebNhutLong.Models;

namespace WebNhutLong.Controllers
{
    public class DonHangController : Controller
    {
        private readonly WebNhutLongEntities db = new WebNhutLongEntities();

        // GET: DonHang
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(db.tbl_DonHang.ToList());
        }

        // GET: DonHang/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_DonHang tbl_DonHang = db.tbl_DonHang.Find(id);
            if (tbl_DonHang == null)
            {
                return HttpNotFound();
            }
            return View(tbl_DonHang);
        }

        // GET: DonHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(
                Include =
                    "ID_Donhang,ID_Products,ID_Customers,CreateDate,ModifyDate,CreateUser,ModifyUser,Status,CodeDonHang,TimeGiaohang,DiaChiGiaoHang,TotalDonHang"
                )] tbl_DonHang tbl_DonHang)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                db.tbl_DonHang.Add(tbl_DonHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_DonHang);
        }

        // GET: DonHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_DonHang tbl_DonHang = db.tbl_DonHang.Find(id);
            if (tbl_DonHang == null)
            {
                return HttpNotFound();
            }
            return View(tbl_DonHang);
        }

        // POST: DonHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                Include =
                    "ID_Donhang,ID_Products,ID_Customers,CreateDate,ModifyDate,CreateUser,ModifyUser,Status,CodeDonHang,TimeGiaohang,DiaChiGiaoHang,TotalDonHang"
                )] tbl_DonHang tbl_DonHang)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(tbl_DonHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_DonHang);
        }

        // GET: DonHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_DonHang tbl_DonHang = db.tbl_DonHang.Find(id);
            if (tbl_DonHang == null)
            {
                return HttpNotFound();
            }
            return View(tbl_DonHang);
        }

        // POST: DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            tbl_DonHang tbl_DonHang = db.tbl_DonHang.Find(id);
            db.tbl_DonHang.Remove(tbl_DonHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChitietDonhang(int id)
        {
            //var query = (from dh in db.tbl_DonHang
            //             join cus in db.tbl_Customers on dh.ID_Customers equals cus.IDCustomers
            //             select new {dh.ID_Donhang,dh.CodeDonHang,cus.NameCustomers }
            //             ).ToList().Select(x=> new tbl_DonHang{ ID_Donhang = x.ID_Donhang,CodeDonHang = x.CodeDonHang});

            IEnumerable<Totalcolumn> query = (from dh in db.tbl_DonHang
                join cus in db.tbl_Customers on dh.ID_Customers equals cus.IDCustomers
                join pro in db.tbl_Products on dh.ID_Products equals pro.ID_Products
                select new
                {
                    dh.ID_Donhang,
                    dh.CodeDonHang,
                    cus.NameCustomers,
                    pro.ID_Products,
                    pro.NameProducts,
                    pro.SolopProducts,
                    pro.LoaigiayProducts,
                    pro.OffsetFlexoProducts,
                    pro.DanKimProducts,
                    pro.SoluongProducts,
                    pro.GiaProducts,
                    pro.CreatedDateProducts,
                    pro.ModifyDateProducts,
                    pro.CreateUserProducts,
                    pro.ModifyUserProducts,
                    pro.StatusProducts,
                    pro.CodeProducts,
                    pro.QuyCachProducts
                }
                ).ToList().Select(
                    x => new Totalcolumn
                    {
                        ID_Donhang = x.ID_Donhang,
                        CodeDonhang = x.CodeDonHang,
                        Namecustomers = x.NameCustomers,
                        ID_Products = x.ID_Products,
                        NameProducts = x.NameProducts,
                        SolopProducts = x.SolopProducts,
                        LoaigiayProducts = x.LoaigiayProducts,
                        OffsetFlexoProducts = x.OffsetFlexoProducts,
                        DanKimProducts = x.DanKimProducts,
                        SoluongProducts = x.SoluongProducts,
                        GiaProducts = x.GiaProducts,
                        CreatedDateProducts = x.CreatedDateProducts,
                        ModifyDateProducts = x.ModifyDateProducts,
                        CreateUserProducts = x.CreateUserProducts,
                        ModifyUserProducts = x.ModifyUserProducts,
                        StatusProducts = x.StatusProducts,
                        CodeProducts = x.CodeProducts,
                        QuyCachProducts = x.QuyCachProducts
                    });

            return View(query);
        }
        public ActionResult BaoCao()
        {
            
            return View();
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
