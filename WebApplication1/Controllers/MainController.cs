﻿using System;
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
        }
        [HttpPost]
        public ActionResult CheckOrder(DonVanChuyen don)
        {
            return View("CreateOrder");
        }
    }
}