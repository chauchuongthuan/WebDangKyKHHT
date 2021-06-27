using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebDangKyKHHT.ViewModels
{
    public class KHHTsViewModel
    {
        [Display(Name = "SinhVien")]
        public int ID_SV { get; set; }

        [Display(Name = "MonHoc")]
        public int IDMH { get; set; } 
        public IEnumerable<SelectListItem> ListofSV { get; set; }

        public IEnumerable<MonHocsViewModel> ListofMH { get; set; }
    }
}