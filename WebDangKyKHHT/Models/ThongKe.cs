//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDangKyKHHT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThongKe
    {
        public int ID { get; set; }
        public Nullable<int> ID_KHHT { get; set; }
        public Nullable<int> ID_SV { get; set; }
    
        public virtual KHHT KHHT { get; set; }
        public virtual SinhVien SinhVien { get; set; }
    }
}