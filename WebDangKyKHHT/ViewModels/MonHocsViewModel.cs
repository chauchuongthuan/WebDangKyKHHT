using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDangKyKHHT.ViewModels
{
    public class MonHocsViewModel
    {
        

        public int IDMH { get; set; }

        public string TenMH { get; set; }

        public string MaMH { get; set; }

        public int SoTinChi { get; set; }

        public int ID_HK { get; set; }
        
        public bool IsSelected { get; set; }

    }
}