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
    
    public partial class KHHT
    {
        public int ID { get; set; }
        
        public int ID_MH { get; set; }
        public string ID_SV { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<int> ID_SVTEST { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual MonHoc MonHoc { get; set; }
    }
}
