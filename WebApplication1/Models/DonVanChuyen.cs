using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DonVanChuyen
    {
        public string MaDon { get; set; }
        public string SDTGiao { get; set; }
        public string DCGiao { get; set; }
        public string TenNguoiNhan { get; set; }
        public string SDTNhan { get; set; }
        public string DCNhan { get; set; }
        public DateTime TGGui { get; set; }
        public DateTime TGNhan { get; set; }
        public bool PTGui { get; set; }
        public int KichCo { get; set; }
        public int KhoiLuong { get; set; }
        public string LoaiHang { get; set; }
    }
}