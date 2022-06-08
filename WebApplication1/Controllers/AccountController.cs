using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        static public string errorMess = "";
        static public bool isSuccess = false;
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpGet]
        public ActionResult Logout()
        {
            if (Session["Username"] != null)
            {
                Session.Clear();
            }
            return Redirect("/");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }
        void connectionString()
        {
            con.ConnectionString = "data source=LAPTOP-J9OI3DHT\\TRUONGCRIS; database=GIAOHANG; integrated security = SSPI;";
        }
        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select username, MatKhau from TaiKhoan where username = '"+acc.UserName+"' and MatKhau = '"+acc.Password+"'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                Session["Username"] = acc.UserName; 
                dr.Close();
                com.CommandText = "select TK_MaKH from TaiKhoan where username = '" + acc.UserName + "'"; //Lay Ma KH tuong ung vs username
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    Session["MaKH"] = dr[0].ToString();
                }
                dr.Close();
                con.Close();
                return Redirect("/Main/CreateOrder");
            }
            con.Close();
            errorMess = "Tên đăng nhập hoặc mật khẩu không đúng.";
            return View("Login");
        }

        [HttpPost]
        public ActionResult CheckAccount(Account acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select username from TaiKhoan where username = '"+acc.UserName+"'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                errorMess = "Tên đăng nhập trùng với tài khoản khác.";
            }
            else
            {
                dr.Close();
                try
                {
                    com.CommandText = "InsertTaiKhoan N'"+acc.HoTen+"', N'"+acc.DiaChi+"', '"+acc.UserName+"', '"+acc.Password+"', '"+acc.Phone+ "', '"+acc.Email+ "', '"+acc.Birthday+"'";
                    com.ExecuteNonQuery();
                    isSuccess = true;
                }
                catch(SqlException ex)
                {
                    errorMess = ex.Message;
                }
            }
            con.Close();
            return View("Register");
        }
    }
}