﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_BaoGia> tbl_BaoGia { get; set; }
        public virtual DbSet<tbl_BaoGiaChiTiet> tbl_BaoGiaChiTiet { get; set; }
        public virtual DbSet<tbl_Customers> tbl_Customers { get; set; }
        public virtual DbSet<tbl_OrderTem> tbl_OrderTem { get; set; }
        public virtual DbSet<tbl_OrderTem_BaoGia> tbl_OrderTem_BaoGia { get; set; }
        public virtual DbSet<tbl_OrderTem_BaoGia_Detail> tbl_OrderTem_BaoGia_Detail { get; set; }
        public virtual DbSet<tbl_Products> tbl_Products { get; set; }
        public virtual DbSet<tbl_QuyTrinh> tbl_QuyTrinh { get; set; }
        public virtual DbSet<tbl_User> tbl_User { get; set; }
    }
}
