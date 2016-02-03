using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNhutLong.Models
{
    public class DonHangView
    {
        public int id { get; set; }
        public string code { get; set; }
        public Nullable<System.DateTime> date_begin { get; set; }
        public Nullable<System.DateTime> date_end { get; set; }
        public Nullable<int> customer_id { get; set; }
        public Nullable<int> status { get; set; }
        public tbl_Customers Customer { get; set; }
        public BaoGiaTemView BaoGiaTemView { get; set; }
        public List<BaoGiaTemView> BaoGiaTemViews { get; set; }
        public List<BaoGiaTemDetailView> BaoGiaTemDetailViews { get; set; }

    }
}