using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DonVanChuyen
    {
        public string MaDon { get; set; }
        public string TenNguoiNhan { get; set; }
        public string SDTNhan { get; set; }
        public string DCNhan { get; set; }
        public int KhoangCach { get; set; }
        public int KhoiLuong { get; set; }
        public int TienThuHo { get; set; }
        public string LoaiHang { get; set; }
        public bool PTGui { get; set; }
        public bool? PTNhan { get; set; }
        public int PhiVanChuyen { get; set; }
        public string TGTaoDon { get; set; }
    }
}