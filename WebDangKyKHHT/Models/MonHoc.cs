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
    
    public partial class MonHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MonHoc()
        {
            this.KHHTs = new HashSet<KHHT>();
        }
    
        public int ID { get; set; }
        
        
        public string MaMH { get; set; }
        
        public string TenMH { get; set; }
        public Nullable<int> SoTinChi { get; set; }
        public Nullable<int> ID_HK { get; set; }
    
        public virtual HocKi HocKi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KHHT> KHHTs { get; set; }
    }
}
