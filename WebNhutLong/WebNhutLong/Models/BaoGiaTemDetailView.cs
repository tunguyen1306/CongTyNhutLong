﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNhutLong.Models
{
    public class BaoGiaTemDetailView
    {
        public int id { get; set; }
        public int ID_Products { get; set; }
        public string NameProducts { get; set; }
        public Nullable<int> SolopProducts { get; set; }
        public string LoaigiayProducts { get; set; }
        public string OffsetFlexoProducts { get; set; }
        public string DanKimProducts { get; set; }
        public string GiaProducts { get; set; }
        public Nullable<System.DateTime> CreatedDateProducts { get; set; }
        public Nullable<System.DateTime> ModifyDateProducts { get; set; }
        public string CreateUserProducts { get; set; }
        public string ModifyUserProducts { get; set; }
        public Nullable<int> StatusProducts { get; set; }
        public string CodeProducts { get; set; }
        public string QuyCachProducts { get; set; }
        public int SoLuong { get; set; }
        public List<tbl_QuyTrinh> QuyTrinhs { get; set; }
    }
}