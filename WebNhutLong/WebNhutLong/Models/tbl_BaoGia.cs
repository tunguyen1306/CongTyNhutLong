//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebNhutLong.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_BaoGia
    {
        public int ID_BaoGia { get; set; }
        public Nullable<int> ID_Customers { get; set; }
        public Nullable<int> BaoGia_ID_Default { get; set; }
        public string TongTien { get; set; }
        public Nullable<int> LamMau { get; set; }
        public Nullable<System.DateTime> NgayCoMau { get; set; }
        public Nullable<System.DateTime> NgayBaoGia { get; set; }
        public Nullable<int> DuyetBaoGia { get; set; }
        public string CodeDonHang { get; set; }
        public Nullable<System.DateTime> NgayBatDauDuKien { get; set; }
        public Nullable<System.DateTime> NgayKetThucDuKien { get; set; }
        public Nullable<System.DateTime> NgayBatDauThucTe { get; set; }
        public Nullable<System.DateTime> NgayKetThucThucTe { get; set; }
        public Nullable<System.DateTime> NgayGiao { get; set; }
        public Nullable<int> QuyTrinh { get; set; }
        public string ChiTietQuyTrinh { get; set; }
        public Nullable<int> LanBaoGia { get; set; }
    }
}
