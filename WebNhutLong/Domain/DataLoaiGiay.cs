using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNhutLong.Models;

namespace WebNhutLong.Domain
{
    public class DataLoaiGiay
    {
        public IEnumerable<LoaiGiay> DataGiay()
        {
            var giay = new List<LoaiGiay>();
            giay.Add(new LoaiGiay{IDLoaigiay = 1,NameLoaiGiay = "3 lớp: 2 Vàng 1 Xeo"});
            giay.Add(new LoaiGiay { IDLoaigiay = 2, NameLoaiGiay = "3 lớp: 1 Trắng 1 Xeo 1 Vàng" });
            giay.Add(new LoaiGiay { IDLoaigiay = 3, NameLoaiGiay = "3 lớp: 1 Đài Loan 1 Xeo 1 Vàng" });
            giay.Add(new LoaiGiay { IDLoaigiay =4, NameLoaiGiay = "5 lớp: 2 Vàng 3 Xeo" });
            giay.Add(new LoaiGiay { IDLoaigiay = 5, NameLoaiGiay = "5 lớp: 1 Trắng 1 Vàng 3 Xeo" });
            giay.Add(new LoaiGiay { IDLoaigiay = 6, NameLoaiGiay = "5 lớp: 1 Đài Loan 1 Vàng 3 Xeo" });
            giay.Add(new LoaiGiay { IDLoaigiay = 7, NameLoaiGiay = "2 lớp: 1 Vàng 1 Xeo - sóng E" });
            giay.Add(new LoaiGiay { IDLoaigiay = 8, NameLoaiGiay = "2 lớp: 1 Vàng 1 Xeo - sóng C" });
            giay.Add(new LoaiGiay { IDLoaigiay = 9, NameLoaiGiay = "2 lớp: 1 Trắng 1 Xeo - sóng E" });
            return giay.ToList();

        }
    }
}