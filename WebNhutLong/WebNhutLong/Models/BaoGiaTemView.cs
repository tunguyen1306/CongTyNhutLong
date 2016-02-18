using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNhutLong.Models
{
    public class BaoGiaTemView
    {
        public int id { get; set; }
        public Nullable<int> order_id { get; set; }
        public Nullable<double> total_money { get; set; }
        public Nullable<System.DateTime> date_begin { get; set; }
        public Nullable<System.DateTime> date_end { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> offset { get; set; }
        public List<BaoGiaTemDetailView> BaoGiaTemDetailViews { get; set; }
        public String note { get; set; }
        public Nullable<int> flow { get; set; }

    }
}