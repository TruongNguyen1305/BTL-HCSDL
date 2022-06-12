using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        static public string errorMess = "";
        static public bool isSuccess = false;
        public ActionResult CreateOrder()
        {
            return View("CreateOrder");
        }
        void connectionString()
        {
            con.ConnectionString = "data source=LAPTOP-J9OI3DHT\\TRUONGCRIS; database=GIAOHANG; integrated security = SSPI;";
            //con.ConnectionString = "Server=tcp:deliverysystem.database.windows.net,1433;Initial Catalog=Delivery_System;Persist Security Info=False;User ID=Henry;Password=Hungnguyen0304@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
        [HttpPost]
        public ActionResult CheckOrder(DonVanChuyen don)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = $"InsertDonVanChuyen N'"+don.TenNguoiNhan+"', '"+don.SDTNhan+"', N'"+don.DCNhan+"', "+don.PTNhan+", '"+Session["MaKH"]+"', "+don.PTGui+", "+don.TienThuHo+", "+don.KhoiLuong+", "+don.KhoangCach+", N'"+don.LoaiHang+"'";
            try
            {
                com.ExecuteNonQuery();
                isSuccess = true;
            }
            catch(SqlException ex)
            {
                errorMess = ex.Message;
            }
            con.Close();
            return View("CreateOrder");
        }

        [HttpGet]
        public ActionResult ListOrders(string TGTaoDon, string PTNhan, string LoaiHang)
        {
            List<DonVanChuyen> Dons = new List<DonVanChuyen>();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from DonVanChuyen where MaKH = '"+Session["MaKH"]+"' ";
            if (!string.IsNullOrEmpty(TGTaoDon))
            {
                com.CommandText += "and TGTaoDon='"+TGTaoDon+"' ";
            }
            if (!string.IsNullOrEmpty(PTNhan))
            {
                int tmp = PTNhan == "True" ? 1 : 0;
                com.CommandText+="and PTNhan="+tmp+"";
            }
            if (!string.IsNullOrEmpty(LoaiHang))
            {
                com.CommandText += "and LoaiHang=N'"+HttpUtility.UrlDecode(LoaiHang)+"'";
            }
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                DonVanChuyen tmp = new DonVanChuyen();
                tmp.MaDon = dr.GetString(0);
                tmp.TenNguoiNhan = dr.GetString(1);
                tmp.SDTNhan = dr.GetString(2);
                tmp.DCNhan = dr.GetString(3);
                tmp.KhoangCach = dr.GetInt32(13);
                tmp.KhoiLuong = dr.GetInt32(11);
                tmp.TienThuHo = dr.GetInt32(9);
                tmp.LoaiHang = dr.GetString(12);
                tmp.PTGui = dr.GetBoolean(8);
                tmp.PTNhan = dr.GetBoolean(4);
                tmp.PhiVanChuyen = dr.GetInt32(10);
                Dons.Add(tmp);
            }
            Session["List"] = Dons;
            con.Close();
            return View("ListOrders");
        }
        public ActionResult DeleteOrder(string MaDon)
        {

            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "delete from DonVanChuyen where MaDon = '"+ MaDon +"'";
            com.ExecuteNonQuery();
            con.Close();
            Session.Remove("List");
            return Redirect("/Main/ListOrders");
        }
        public ActionResult UpdateOrder(DonVanChuyen don)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "UpdateDonVanChuyen '"+don.MaDon+"', N'"+don.TenNguoiNhan+"', '"+don.SDTNhan+"', N'"+don.DCNhan+"', '"+don.PTNhan+"', '"+don.TienThuHo+"'";
            com.ExecuteNonQuery();
            con.Close();
            Session.Remove("List");
            return Redirect("/Main/ListOrders");
        }

        public ActionResult Filter(DonVanChuyen don)
        {
            return Redirect(@"/Main/ListOrders?TGTaoDon="+don.TGTaoDon+"&PTNhan="+don.PTNhan+"&LoaiHang="+don.LoaiHang+"");
        }
    }
}