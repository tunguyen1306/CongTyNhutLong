using WebNhutLong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNhutLong.Domain
{
    public class Data
    {
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
           
            menu.Add(new Navbar { Id = 19, nameOption = "Khách Hàng", controller = "Customers", action = "Index", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 20, nameOption = "Sản Phẩm", controller = "Products", action = "Index", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 21, nameOption = "Đơn hàng", controller = "DonHang", action = "Index", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 22, nameOption = "Lịch sản xuất", controller = "Sanxuat", action = "Index", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 23, nameOption = "Báo cáo", controller = "DonHang", action = "BaoCao", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = false, parentId = 0 });
           

            return menu.ToList();
        }
    }
}