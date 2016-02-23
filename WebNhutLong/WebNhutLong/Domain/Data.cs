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
            //menu.Add(new Navbar { Id = 20, nameOption = "Sản Phẩm", controller = "Products", action = "Index", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = false, parentId = 0 });
           
            menu.Add(new Navbar { Id = 22, nameOption = "Lịch sản xuất", controller = "Sanxuat", action = "Index", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 23, nameOption = "Báo cáo", controller = "BaoCao", action = "Index", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 24, nameOption = "Báo cáo tổng quát cho khâu sản xuất", controller = "BaoCao", action = "Index", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = false, parentId = 23 });
            menu.Add(new Navbar { Id = 25, nameOption = "Báo cáo tổng quát khâu kinh doanh", controller = "BaoCao", action = "Index", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = false, parentId = 23 });
           

            return menu.ToList();
        }
    }
}