//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebNhutLong.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_OrderTem
    {
        public int id { get; set; }
        public Nullable<int> customer_id { get; set; }
        public string code { get; set; }
        public Nullable<System.DateTime> date_begin { get; set; }
        public Nullable<System.DateTime> date_end { get; set; }
        public Nullable<System.DateTime> date_begin_plan { get; set; }
        public Nullable<System.DateTime> date_end_plan { get; set; }
        public Nullable<System.DateTime> date_deliver { get; set; }
        public string address_deliver { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public string create_user { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public string update_user { get; set; }
    }
}
