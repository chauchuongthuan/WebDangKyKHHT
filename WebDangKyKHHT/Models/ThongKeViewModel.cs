using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDangKyKHHT.Models
{
    public class ThongKeViewModel
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